using Microsoft.EntityFrameworkCore;
using Oficina.OrdemServico.Infrastructure;
using Oficina.OrdemServico.Domain;
using Oficina.OrdemServico.Api;
using FluentValidation;

namespace Oficina.OrdemServico;

public static class Endpoints
{
    public static void MapOrdemServicoEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/ordens").WithTags("Ordem de Serviço");

        g.MapGet("/", async (OrdemServicoDbContext db) =>
            Results.Ok(await db.Ordens.AsNoTracking().Include(o => o.Itens).ToListAsync())).WithSummary("Lista ordens de serviço");

        g.MapPost("/", async (OrdemCreateDto dto, OrdemServicoDbContext db, IValidator<OrdemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var os = new OrdemServico.Domain.OrdemServico{ ClienteId=dto.ClienteId, MecanicoId=dto.MecanicoId, DescricaoProblema=dto.DescricaoProblema };
            db.Ordens.Add(os); await db.SaveChangesAsync(); return Results.Created($"/ordens/{os.Id}", os);
        }).WithSummary("Cria OS");

        g.MapPost("/{id:guid}/itens", async (Guid id, ItemCreateDto dto, OrdemServicoDbContext db, IValidator<ItemCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            if (await db.Ordens.FindAsync(id) is null) return Results.NotFound("OS não encontrada");
            var item = new ItemServico{ OrdemServicoId=id, PecaId=dto.PecaId, Descricao=dto.Descricao, Quantidade=dto.Quantidade, ValorUnitario=dto.ValorUnitario };
            db.Itens.Add(item); await db.SaveChangesAsync(); return Results.Created($"/ordens/{id}/itens/{item.Id}", item);
        }).WithSummary("Adiciona item na OS");

        g.MapPut("/{id:guid}/status", async (Guid id, string status, OrdemServicoDbContext db) =>
        {
            var os = await db.Ordens.FindAsync(id); if (os is null) return Results.NotFound();
            os.Status = status.ToUpper(); if (os.Status=="CONCLUIDA") os.DataConclusao=DateTime.UtcNow; os.Touch();
            await db.SaveChangesAsync(); return Results.Ok(os);
        }).WithSummary("Altera status da OS");
    }
}
