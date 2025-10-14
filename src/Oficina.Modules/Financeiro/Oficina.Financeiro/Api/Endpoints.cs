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
        var g = app.MapGroup("/financeiro").WithTags("Financeiro");

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
    }
}

