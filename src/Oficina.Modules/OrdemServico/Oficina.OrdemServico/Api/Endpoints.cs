using Microsoft.EntityFrameworkCore;
using Oficina.OrdemServico.Infrastructure;
using Oficina.OrdemServico.Domain;
using Oficina.OrdemServico.Api;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Oficina.OrdemServico;

public static class Endpoints
{
    public static void MapOrdemServicoEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/ordens").WithTags("Ordem de ServiÃ§o");

        g.MapGet("/", async (OrdemServicoDbContext db) =>
            Results.Ok(await db.Ordens.AsNoTracking().Include(o => o.Itens).ToListAsync())).WithSummary("Lista ordens de serviÃ§o");

        g.MapPost("/", async (OrdemCreateDto dto, OrdemServicoDbContext db, IValidator<OrdemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var os = new OrdemServico.Domain.OrdemServico{ Cliente_Id=dto.Cliente_Id, Mecanico_Id=dto.MecanicoId, Descricao_Problema=dto.DescricaoProblema };
            db.Ordens.Add(os); await db.SaveChangesAsync(); return Results.Created($"/ordens/{os.Id}", os);
        }).WithSummary("Cria OS");

        g.MapPost("/{id:long}/itens", async (long id, ItemCreateDto dto, OrdemServicoDbContext db, IValidator<ItemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            if (await db.Ordens.FindAsync(id) is null) return Results.NotFound("OS nÃ£o encontrada");
            var item = new ItemServico{ Ordem_Servico_Id=id, Peca_Id=dto.PecaId, Descricao=dto.Descricao, Quantidade=dto.Quantidade, Valor_Unitario=dto.ValorUnitario };
            db.Itens.Add(item); await db.SaveChangesAsync(); return Results.Created($"/ordens/{id}/itens/{item.Id}", item);
        }).WithSummary("Adiciona item na OS");

        g.MapPut("/{id:long}/status", async (long id, string status, OrdemServicoDbContext db) =>
        {
            var os = await db.Ordens.FindAsync(id); if (os is null) return Results.NotFound();
            os.Status = status.ToUpper(); if (os.Status=="CONCLUIDA") os.Data_Conclusao=DateTime.UtcNow; os.Touch();
            await db.SaveChangesAsync(); return Results.Ok(os);
        }).WithSummary("Altera status da OS");
    }
}

