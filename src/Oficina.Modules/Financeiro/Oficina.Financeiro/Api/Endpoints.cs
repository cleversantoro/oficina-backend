using Microsoft.EntityFrameworkCore;
using Oficina.Financeiro.Infrastructure;
using Oficina.Financeiro.Domain;
using Oficina.Financeiro.Api;
using Oficina.Financeiro.Infrastructure.External;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Oficina.Financeiro;

public static class Endpoints
{
    public static void MapFinanceiroEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/financeiro").WithTags("Financeiro - Pagamentos");
        g.MapPost("/pagamentos", async (PagamentoCreateDto dto, FinanceiroDbContext db, IValidator<PagamentoCreateDto> v, IPaymentGatewayClient gateway) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var (ok, tx) = await gateway.ChargeAsync(dto.Meio.ToUpper(), dto.Valor);
            var p = new Pagamento{ Ordem_Servico_Id=dto.OrdemServicoId, Meio=dto.Meio.ToUpper(), Valor=dto.Valor, Status = ok ? "PAGO" : "FALHA", Transacao_Id = tx };
            db.Pagamentos.Add(p); await db.SaveChangesAsync(); return Results.Created($"/financeiro/pagamentos/{p.Id}", p);
        }).WithSummary("Cria pagamento e processa no gateway (mock)");
        g.MapPut("/pagamentos/{id:long}/status", async (long id, AtualizaStatusDto dto, FinanceiroDbContext db, IValidator<AtualizaStatusDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var pg = await db.Pagamentos.FindAsync(id); if (pg is null) return Results.NotFound();
            pg.Status = dto.Status.ToUpper(); pg.Touch(); await db.SaveChangesAsync(); return Results.Ok(pg);
        }).WithSummary("Atualiza status do pagamento");
        g.MapPost("/nfe", async (NfeCreateDto dto, FinanceiroDbContext db, IValidator<NfeCreateDto> v, INfeClient nfeClient) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var valor = await db.Pagamentos.Where(p=>p.Ordem_Servico_Id==dto.OrdemServicoId && p.Status=="PAGO").SumAsync(p=>p.Valor);
            var (ok, numero, chave) = await nfeClient.EmitirAsync(dto.OrdemServicoId, valor);
            var n = new NFe{ Ordem_Servico_Id=dto.OrdemServicoId, Numero=numero, Chave_Acesso=chave, Status = ok ? "EMITIDA" : "FALHA" };
            db.NFes.Add(n); await db.SaveChangesAsync(); return Results.Created($"/financeiro/nfe/{n.Id}", n);
        }).WithSummary("Emite NF-e (mock)");

        var m = app.MapGroup("/financeiro").WithTags("Financeiro - Métodos");
        m.MapGet("/metodos", async (FinanceiroDbContext db) => Results.Ok(await db.MetodosPagamento.AsNoTracking().ToListAsync())).WithSummary("Lista métodos de pagamento");
        m.MapPost("/metodos", async (MetodoPagamentoDto dto, FinanceiroDbContext db) => {
            var m = new MetodoPagamento { Nome = dto.Nome, Descricao = dto.Descricao };
            db.MetodosPagamento.Add(m); await db.SaveChangesAsync(); return Results.Created($"/financeiro/metodos/{m.Id}", m);
        }).WithSummary("Cria método de pagamento");
        m.MapPut("/metodos/{id:long}", async (long id, MetodoPagamentoDto dto, FinanceiroDbContext db) => {
            var m = await db.MetodosPagamento.FindAsync(id); if (m is null) return Results.NotFound();
            m.Nome = dto.Nome; m.Descricao = dto.Descricao; await db.SaveChangesAsync(); return Results.Ok(m);
        }).WithSummary("Atualiza método de pagamento");
        m.MapDelete("/metodos/{id:long}", async (long id, FinanceiroDbContext db) => {
            var m = await db.MetodosPagamento.FindAsync(id); if (m is null) return Results.NotFound(); db.MetodosPagamento.Remove(m); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui método de pagamento");

        var cp = app.MapGroup("/financeiro").WithTags("Financeiro - Contas a Pagar");
        cp.MapGet("/contas-pagar", async (FinanceiroDbContext db) => Results.Ok(await db.ContasPagar.AsNoTracking().ToListAsync())).WithSummary("Lista contas a pagar");
        cp.MapPost("/contas-pagar", async (ContaPagarDto dto, FinanceiroDbContext db) => {
            var c = new ContaPagar { Fornecedor_Id = dto.FornecedorId, Descricao = dto.Descricao, Valor = dto.Valor, Vencimento = dto.Vencimento, Status = dto.Status, Data_Pagamento = dto.DataPagamento, Metodo_Id = dto.MetodoId, Observacao = dto.Observacao };
            db.ContasPagar.Add(c); await db.SaveChangesAsync(); return Results.Created($"/financeiro/contas-pagar/{c.Id}", c);
        }).WithSummary("Cria conta a pagar");
        cp.MapPut("/contas-pagar/{id:long}", async (long id, ContaPagarDto dto, FinanceiroDbContext db) => {
            var c = await db.ContasPagar.FindAsync(id); if (c is null) return Results.NotFound();
            c.Fornecedor_Id = dto.FornecedorId; c.Descricao = dto.Descricao; c.Valor = dto.Valor; c.Vencimento = dto.Vencimento; c.Status = dto.Status; c.Data_Pagamento = dto.DataPagamento; c.Metodo_Id = dto.MetodoId; c.Observacao = dto.Observacao; await db.SaveChangesAsync(); return Results.Ok(c);
        }).WithSummary("Atualiza conta a pagar");
        cp.MapDelete("/contas-pagar/{id:long}", async (long id, FinanceiroDbContext db) => {
            var c = await db.ContasPagar.FindAsync(id); if (c is null) return Results.NotFound(); db.ContasPagar.Remove(c); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui conta a pagar");

        var cr = app.MapGroup("/financeiro").WithTags("Financeiro - Contas a Receber");
        cr.MapGet("/contas-receber", async (FinanceiroDbContext db) => Results.Ok(await db.ContasReceber.AsNoTracking().ToListAsync())).WithSummary("Lista contas a receber");
        cr.MapPost("/contas-receber", async (ContaReceberDto dto, FinanceiroDbContext db) => {
            var c = new ContaReceber { Cliente_Id = dto.ClienteId, Descricao = dto.Descricao, Valor = dto.Valor, Vencimento = dto.Vencimento, Status = dto.Status, Data_Recebimento = dto.DataRecebimento, Metodo_Id = dto.MetodoId, Observacao = dto.Observacao };
            db.ContasReceber.Add(c); await db.SaveChangesAsync(); return Results.Created($"/financeiro/contas-receber/{c.Id}", c);
        }).WithSummary("Cria conta a receber");
        cr.MapPut("/contas-receber/{id:long}", async (long id, ContaReceberDto dto, FinanceiroDbContext db) => {
            var c = await db.ContasReceber.FindAsync(id); if (c is null) return Results.NotFound();
            c.Cliente_Id = dto.ClienteId; c.Descricao = dto.Descricao; c.Valor = dto.Valor; c.Vencimento = dto.Vencimento; c.Status = dto.Status; c.Data_Recebimento = dto.DataRecebimento; c.Metodo_Id = dto.MetodoId; c.Observacao = dto.Observacao; await db.SaveChangesAsync(); return Results.Ok(c);
        }).WithSummary("Atualiza conta a receber");
        cr.MapDelete("/contas-receber/{id:long}", async (long id, FinanceiroDbContext db) => {
            var c = await db.ContasReceber.FindAsync(id); if (c is null) return Results.NotFound(); db.ContasReceber.Remove(c); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui conta a receber");

        var l = app.MapGroup("/financeiro").WithTags("Financeiro - Lançamentos");
        l.MapGet("/lancamentos", async (FinanceiroDbContext db) => Results.Ok(await db.Lancamentos.AsNoTracking().ToListAsync())).WithSummary("Lista lançamentos");
        l.MapPost("/lancamentos", async (LancamentoDto dto, FinanceiroDbContext db) => {
            var l = new Lancamento { Tipo = dto.Tipo, Descricao = dto.Descricao, Valor = dto.Valor, Data_Lancamento = dto.DataLancamento, Referencia = dto.Referencia, Observacao = dto.Observacao };
            db.Lancamentos.Add(l); await db.SaveChangesAsync(); return Results.Created($"/financeiro/lancamentos/{l.Id}", l);
        }).WithSummary("Cria lançamento");
        l.MapPut("/lancamentos/{id:long}", async (long id, LancamentoDto dto, FinanceiroDbContext db) => {
            var l = await db.Lancamentos.FindAsync(id); if (l is null) return Results.NotFound();
            l.Tipo = dto.Tipo; l.Descricao = dto.Descricao; l.Valor = dto.Valor; l.Data_Lancamento = dto.DataLancamento; l.Referencia = dto.Referencia; l.Observacao = dto.Observacao; await db.SaveChangesAsync(); return Results.Ok(l);
        }).WithSummary("Atualiza lançamento");
        l.MapDelete("/lancamentos/{id:long}", async (long id, FinanceiroDbContext db) => {
            var l = await db.Lancamentos.FindAsync(id); if (l is null) return Results.NotFound(); db.Lancamentos.Remove(l); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui lançamento");

        var a = app.MapGroup("/financeiro").WithTags("Financeiro - Anexos");
        a.MapGet("/anexos", async (FinanceiroDbContext db) => Results.Ok(await db.Anexos.AsNoTracking().ToListAsync())).WithSummary("Lista anexos financeiros");
        a.MapPost("/anexos", async (FinanceiroAnexoDto dto, FinanceiroDbContext db) => {
            var a = new FinanceiroAnexo { Pagamento_Id = dto.PagamentoId, Conta_Pagar_Id = dto.ContaPagarId, Conta_Receber_Id = dto.ContaReceberId, Nome = dto.Nome, Tipo = dto.Tipo, Url = dto.Url, Observacao = dto.Observacao };
            db.Anexos.Add(a); await db.SaveChangesAsync(); return Results.Created($"/financeiro/anexos/{a.Id}", a);
        }).WithSummary("Cria anexo financeiro");
        a.MapDelete("/anexos/{id:long}", async (long id, FinanceiroDbContext db) => {
            var a = await db.Anexos.FindAsync(id); if (a is null) return Results.NotFound(); db.Anexos.Remove(a); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui anexo financeiro");

        var h = app.MapGroup("/financeiro").WithTags("Financeiro - Histórico");
        h.MapGet("/historico", async (FinanceiroDbContext db) => Results.Ok(await db.Historicos.AsNoTracking().ToListAsync())).WithSummary("Lista histórico financeiro");
        h.MapPost("/historico", async (FinanceiroHistoricoDto dto, FinanceiroDbContext db) => {
            var h = new FinanceiroHistorico { Entidade = dto.Entidade, Entidade_Id = dto.EntidadeId, Data_Alteracao = dto.DataAlteracao, Usuario = dto.Usuario, Campo = dto.Campo, Valor_Antigo = dto.ValorAntigo, Valor_Novo = dto.ValorNovo };
            db.Historicos.Add(h); await db.SaveChangesAsync(); return Results.Created($"/financeiro/historico/{h.Id}", h);
        }).WithSummary("Cria histórico financeiro");
        h.MapDelete("/historico/{id:long}", async (long id, FinanceiroDbContext db) => {
            var h = await db.Historicos.FindAsync(id); if (h is null) return Results.NotFound(); db.Historicos.Remove(h); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui histórico financeiro");
    }
}


