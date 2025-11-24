using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain;
using Oficina.Cadastro.Infrastructure;
using Oficina.OrdemServico.Api;
using Oficina.OrdemServico.Domain;
using Oficina.OrdemServico.Infrastructure;

namespace Oficina.OrdemServico;

public static class Endpoints
{
    public static void MapOrdemServicoEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/ordens").WithTags("Ordem de Serviço - Ordem");
        g.MapGet("/", async (OrdemServicoDbContext db) =>
             Results.Ok(await db.Ordens
                .Include(o => o.Itens)
                .Include(o => o.Anexos)
                .Include(o => o.Historicos)
                .Include(o => o.Checklists)
                .Include(o => o.Avaliacoes)
                .Include(o => o.Pagamentos)
                .Include(o => o.Observacoes)
                //.Include(o => o.Cliente)
                //.Include(o => o.Mecanico)
                .AsNoTracking().ToListAsync()))
             .WithSummary("Lista ordens de serviço");
        g.MapGet("/{id:long}", async (long id, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens
                .Include(o => o.Itens)
                .Include(o => o.Anexos)
                .Include(o => o.Historicos)
                .Include(o => o.Checklists)
                .Include(o => o.Avaliacoes)
                .Include(o => o.Pagamentos)
                .Include(o => o.Observacoes)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            return ordem is null ? Results.NotFound() : Results.Ok(ordem);
        }).WithSummary("Detalhes da ordem de serviço");
        g.MapPost("/", async (OrdemCreateDto dto, OrdemServicoDbContext db, IValidator<OrdemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var os = new OrdemServico.Domain.OrdemServico{
                Cliente_Id = dto.ClienteId,
                Mecanico_Id = dto.MecanicoId,
                Descricao_Problema = dto.DescricaoProblema,
                Itens = dto.Itens?.Select(i => new ItemServico{
                    Peca_Id = i.Peca_Id,
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    Valor_Unitario = i.ValorUnitario
                }).ToList() ?? new(),
                Anexos = dto.Anexos?.Select(a => new OrdemServicoAnexo{
                    Nome = a.Nome,
                    Tipo = a.Tipo,
                    Url = a.Url,
                    Observacao = a.Observacao
                }).ToList() ?? new(),
                Checklists = dto.Checklists?.Select(c => new OrdemServicoChecklist{
                    Item = c.Item,
                    Realizado = c.Realizado,
                    Observacao = c.Observacao
                }).ToList() ?? new()
            };
            db.Ordens.Add(os); await db.SaveChangesAsync(); return Results.Created($"/ordens/{os.Id}", os);
        }).WithSummary("Cria OS");
        g.MapPut("/{id:long}", async (long id, OrdemCreateDto dto, OrdemServicoDbContext db, IValidator<OrdemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var os = await db.Ordens
                .Include(o => o.Itens)
                .Include(o => o.Anexos)
                .Include(o => o.Checklists)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (os is null) return Results.NotFound();
            os.Cliente_Id = dto.ClienteId;
            os.Mecanico_Id = dto.MecanicoId;
            os.Descricao_Problema = dto.DescricaoProblema;
            db.Itens.RemoveRange(os.Itens);
            db.Set<OrdemServicoAnexo>().RemoveRange(os.Anexos);
            db.Set<OrdemServicoChecklist>().RemoveRange(os.Checklists);
            os.Itens = dto.Itens?.Select(i => new ItemServico{
                Peca_Id = i.Peca_Id,
                Descricao = i.Descricao,
                Quantidade = i.Quantidade,
                Valor_Unitario = i.ValorUnitario
            }).ToList() ?? new();
            os.Anexos = dto.Anexos?.Select(a => new OrdemServicoAnexo{
                Nome = a.Nome,
                Tipo = a.Tipo,
                Url = a.Url,
                Observacao = a.Observacao
            }).ToList() ?? new();
            os.Checklists = dto.Checklists?.Select(c => new OrdemServicoChecklist{
                Item = c.Item,
                Realizado = c.Realizado,
                Observacao = c.Observacao
            }).ToList() ?? new();
            await db.SaveChangesAsync();
            return Results.Ok(os);
        }).WithSummary("Atualiza OS");
        g.MapDelete("/{id:long}", async (long id, OrdemServicoDbContext db) =>
        {
            var os = await db.Ordens.FirstOrDefaultAsync(o => o.Id == id);
            if (os is null) return Results.NotFound();
            db.Ordens.Remove(os);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui OS");

        // Anexos
        var a = app.MapGroup("/ordens").WithTags("Ordem de Serviço - Anexos");
        a.MapPost("/{id:long}/anexos", async (long id, OrdemServicoAnexoDto dto, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens.Include(o => o.Anexos).FirstOrDefaultAsync(o => o.Id == id);
            if (ordem is null) return Results.NotFound();
            var anexo = new OrdemServicoAnexo { Ordem_Servico_Id = id, Nome = dto.Nome, Tipo = dto.Tipo, Url = dto.Url, Observacao = dto.Observacao };
            ordem.Anexos.Add(anexo);
            await db.SaveChangesAsync();
            return Results.Created($"/ordens/{id}/anexos/{anexo.Id}", anexo);
        }).WithSummary("Adiciona anexo à OS");
        a.MapDelete("/{id:long}/anexos/{anexoId:long}", async (long id, long anexoId, OrdemServicoDbContext db) =>
        {
            var anexo = await db.Set<OrdemServicoAnexo>().FirstOrDefaultAsync(a => a.Ordem_Servico_Id == id && a.Id == anexoId);
            if (anexo is null) return Results.NotFound();
            db.Set<OrdemServicoAnexo>().Remove(anexo);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Remove anexo da OS");

        // Checklists
        var c = app.MapGroup("/ordens").WithTags("Ordem de Serviço - ChecList");
        c.MapPost("/{id:long}/checklists", async (long id, OrdemServicoChecklistDto dto, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens.Include(o => o.Checklists).FirstOrDefaultAsync(o => o.Id == id);
            if (ordem is null) return Results.NotFound();
            var checklist = new OrdemServicoChecklist { Ordem_Servico_Id = id, Item = dto.Item, Realizado = dto.Realizado, Observacao = dto.Observacao };
            ordem.Checklists.Add(checklist);
            await db.SaveChangesAsync();
            return Results.Created($"/ordens/{id}/checklists/{checklist.Id}", checklist);
        }).WithSummary("Adiciona checklist à OS");
        c.MapDelete("/{id:long}/checklists/{checklistId:long}", async (long id, long checklistId, OrdemServicoDbContext db) =>
        {
            var checklist = await db.Set<OrdemServicoChecklist>().FirstOrDefaultAsync(c => c.Ordem_Servico_Id == id && c.Id == checklistId);
            if (checklist is null) return Results.NotFound();
            db.Set<OrdemServicoChecklist>().Remove(checklist);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Remove checklist da OS");

        // Avaliações
        var av = app.MapGroup("/ordens").WithTags("Ordem de Serviço - Avaliações");
        av.MapPost("/{id:long}/avaliacoes", async (long id, OrdemServicoAvaliacaoDto dto, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens.Include(o => o.Avaliacoes).FirstOrDefaultAsync(o => o.Id == id);
            if (ordem is null) return Results.NotFound();
            var avaliacao = new OrdemServicoAvaliacao { Ordem_Servico_Id = id, Nota = dto.Nota, Comentario = dto.Comentario, Usuario = dto.Usuario };
            ordem.Avaliacoes.Add(avaliacao);
            await db.SaveChangesAsync();
            return Results.Created($"/ordens/{id}/avaliacoes/{avaliacao.Id}", avaliacao);
        }).WithSummary("Adiciona avaliação à OS");
        av.MapDelete("/{id:long}/avaliacoes/{avaliacaoId:long}", async (long id, long avaliacaoId, OrdemServicoDbContext db) =>
        {
            var avaliacao = await db.Set<OrdemServicoAvaliacao>().FirstOrDefaultAsync(a => a.Ordem_Servico_Id == id && a.Id == avaliacaoId);
            if (avaliacao is null) return Results.NotFound();
            db.Set<OrdemServicoAvaliacao>().Remove(avaliacao);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Remove avaliação da OS");

        // Pagamentos
        var p = app.MapGroup("/ordens").WithTags("Ordem de Serviço - Pagamentos");
        p.MapPost("/{id:long}/pagamentos", async (long id, OrdemServicoPagamentoDto dto, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens.Include(o => o.Pagamentos).FirstOrDefaultAsync(o => o.Id == id);
            if (ordem is null) return Results.NotFound();
            var pagamento = new OrdemServicoPagamento { Ordem_Servico_Id = id, Valor = dto.Valor, Status = dto.Status, Data_Pagamento = dto.DataPagamento, Metodo = dto.Metodo, Observacao = dto.Observacao };
            ordem.Pagamentos.Add(pagamento);
            await db.SaveChangesAsync();
            return Results.Created($"/ordens/{id}/pagamentos/{pagamento.Id}", pagamento);
        }).WithSummary("Adiciona pagamento à OS");
        p.MapDelete("/{id:long}/pagamentos/{pagamentoId:long}", async (long id, long pagamentoId, OrdemServicoDbContext db) =>
        {
            var pagamento = await db.Set<OrdemServicoPagamento>().FirstOrDefaultAsync(p => p.Ordem_Servico_Id == id && p.Id == pagamentoId);
            if (pagamento is null) return Results.NotFound();
            db.Set<OrdemServicoPagamento>().Remove(pagamento);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Remove pagamento da OS");

        // Observações
        var o = app.MapGroup("/ordens").WithTags("Ordem de Serviço - Observações");
        o.MapPost("/{id:long}/observacoes", async (long id, OrdemServicoObservacaoDto dto, OrdemServicoDbContext db) =>
        {
            var ordem = await db.Ordens.Include(o => o.Observacoes).FirstOrDefaultAsync(o => o.Id == id);
            if (ordem is null) return Results.NotFound();
            var obs = new OrdemServicoObservacao { Ordem_Servico_Id = id, Usuario = dto.Usuario, Texto = dto.Texto };
            ordem.Observacoes.Add(obs);
            await db.SaveChangesAsync();
            return Results.Created($"/ordens/{id}/observacoes/{obs.Id}", obs);
        }).WithSummary("Adiciona observação à OS");
        o.MapDelete("/{id:long}/observacoes/{obsId:long}", async (long id, long obsId, OrdemServicoDbContext db) =>
        {
            var obs = await db.Set<OrdemServicoObservacao>().FirstOrDefaultAsync(o => o.Ordem_Servico_Id == id && o.Id == obsId);
            if (obs is null) return Results.NotFound();
            db.Set<OrdemServicoObservacao>().Remove(obs);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Remove observação da OS");
    }

}


