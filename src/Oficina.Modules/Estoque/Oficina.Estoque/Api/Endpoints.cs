using Microsoft.EntityFrameworkCore;
using Oficina.Estoque.Infrastructure;
using Oficina.Estoque.Domain;
using Oficina.Estoque.Api;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Oficina.Estoque;

public static class Endpoints
{
    public static void MapEstoqueEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/estoque").WithTags("Estoque");

        g.MapGet("/pecas", async (EstoqueDbContext db) => Results.Ok(await db.Pecas.AsNoTracking().ToListAsync())).WithSummary("Lista peÃ§as");
        g.MapPost("/pecas", async (PecaCreateDto dto, EstoqueDbContext db, IValidator<PecaCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var p = new Peca{ Codigo=dto.Codigo, Descricao=dto.Descricao, Preco_Unitario=dto.PrecoUnitario, Quantidade=dto.Quantidade, Fornecedor_Id=dto.FornecedorId };
            db.Pecas.Add(p); await db.SaveChangesAsync(); return Results.Created($"/estoque/pecas/{p.Id}", p);
        }).WithSummary("Cria peÃ§a");

        g.MapPost("/movimentacoes", async (MovimentacaoCreateDto dto, EstoqueDbContext db, IValidator<MovimentacaoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var m = new Movimentacao{ Peca_Id=dto.PecaId, Quantidade=dto.Quantidade, Tipo=dto.Tipo, Referencia=dto.Referencia };
            db.Movimentacoes.Add(m);
            var peca = await db.Pecas.FindAsync(dto.PecaId);
            if (peca is null) return Results.BadRequest("PeÃ§a nÃ£o encontrada");
            if (dto.Tipo.ToUpper() == "ENTRADA") peca.Quantidade += dto.Quantidade;
            else if (dto.Tipo.ToUpper() == "SAIDA") peca.Quantidade -= dto.Quantidade;
            peca.Touch();
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/movimentacoes/{m.Id}", m);
        }).WithSummary("LanÃ§a movimentaÃ§Ã£o de estoque");
    }
}

