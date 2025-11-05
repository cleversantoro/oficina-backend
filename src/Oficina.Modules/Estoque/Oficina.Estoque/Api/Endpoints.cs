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

        g.MapGet("/pecas", async (EstoqueDbContext db) =>
            Results.Ok(await db.Pecas
                .Include(p => p.Fornecedores)
                .Include(p => p.Anexos)
                .Include(p => p.Historicos)
                .AsNoTracking().ToListAsync()))
            .WithSummary("Lista peças");
        g.MapGet("/pecas/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var peca = await db.Pecas
                .Include(p => p.Fornecedores)
                .Include(p => p.Anexos)
                .Include(p => p.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            return peca is null ? Results.NotFound() : Results.Ok(peca);
        }).WithSummary("Detalhes da peça");
        g.MapPost("/pecas", async (PecaCreateDto dto, EstoqueDbContext db, IValidator<PecaCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var p = new Peca{
                Codigo = dto.Codigo,
                Descricao = dto.Descricao,
                Preco_Unitario = dto.PrecoUnitario,
                Quantidade = dto.Quantidade,
                Estoque_Minimo = dto.EstoqueMinimo,
                Estoque_Maximo = dto.EstoqueMaximo,
                Unidade = dto.Unidade,
                Status = dto.Status,
                Observacoes = dto.Observacoes,
                Fabricante_Id = dto.FabricanteId,
                Categoria_Id = dto.CategoriaId,
                Localizacao_Id = dto.LocalizacaoId,
                Fornecedores = dto.Fornecedores?.Select(f => new PecaFornecedor {
                    Fornecedor_Id = f.FornecedorId,
                    Preco = f.Preco,
                    Prazo_Entrega = f.PrazoEntrega,
                    Observacao = f.Observacao
                }).ToList() ?? new List<PecaFornecedor>(),
                Anexos = dto.Anexos?.Select(a => new PecaAnexo {
                    Nome = a.Nome,
                    Tipo = a.Tipo,
                    Url = a.Url,
                    Observacao = a.Observacao
                }).ToList() ?? new List<PecaAnexo>()
            };
            db.Pecas.Add(p); await db.SaveChangesAsync(); return Results.Created($"/estoque/pecas/{p.Id}", p);
        }).WithSummary("Cria peça");

        g.MapPut("/pecas/{id:long}", async (long id, PecaCreateDto dto, EstoqueDbContext db, IValidator<PecaCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var peca = await db.Pecas
                .Include(p => p.Fornecedores)
                .Include(p => p.Anexos)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (peca is null) return Results.NotFound();
            peca.Codigo = dto.Codigo;
            peca.Descricao = dto.Descricao;
            peca.Preco_Unitario = dto.PrecoUnitario;
            peca.Quantidade = dto.Quantidade;
            peca.Estoque_Minimo = dto.EstoqueMinimo;
            peca.Estoque_Maximo = dto.EstoqueMaximo;
            peca.Unidade = dto.Unidade;
            peca.Status = dto.Status;
            peca.Observacoes = dto.Observacoes;
            peca.Fabricante_Id = dto.FabricanteId;
            peca.Categoria_Id = dto.CategoriaId;
            peca.Localizacao_Id = dto.LocalizacaoId;
            db.PecasFornecedores.RemoveRange(peca.Fornecedores);
            db.PecasAnexos.RemoveRange(peca.Anexos);
            peca.Fornecedores = dto.Fornecedores?.Select(f => new PecaFornecedor {
                Fornecedor_Id = f.FornecedorId,
                Preco = f.Preco,
                Prazo_Entrega = f.PrazoEntrega,
                Observacao = f.Observacao
            }).ToList() ?? new List<PecaFornecedor>();
            peca.Anexos = dto.Anexos?.Select(a => new PecaAnexo {
                Nome = a.Nome,
                Tipo = a.Tipo,
                Url = a.Url,
                Observacao = a.Observacao
            }).ToList() ?? new List<PecaAnexo>();
            await db.SaveChangesAsync();
            return Results.Ok(peca);
        }).WithSummary("Atualiza peça");

        g.MapDelete("/pecas/{id:long}", async (long id, EstoqueDbContext db) => {
            var peca = await db.Pecas.FirstOrDefaultAsync(p => p.Id == id);
            if (peca is null) return Results.NotFound();
            db.Pecas.Remove(peca);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui peça");

        g.MapGet("/fabricantes", async (EstoqueDbContext db) => Results.Ok(await db.Fabricantes.AsNoTracking().ToListAsync())).WithSummary("Lista fabricantes");
        g.MapGet("/fabricantes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
            return fab is null ? Results.NotFound() : Results.Ok(fab);
        }).WithSummary("Detalhes do fabricante");
        g.MapPost("/fabricantes", async (FabricanteDto dto, EstoqueDbContext db) =>
        {
            var fab = new Fabricante { Nome = dto.Nome, Cnpj = dto.Cnpj, Contato = dto.Contato };
            db.Fabricantes.Add(fab);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/fabricantes/{fab.Id}", fab);
        }).WithSummary("Cria fabricante");
        g.MapPut("/fabricantes/{id:long}", async (long id, FabricanteDto dto, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.FindAsync(id);
            if (fab is null) return Results.NotFound();
            fab.Nome = dto.Nome;
            fab.Cnpj = dto.Cnpj;
            fab.Contato = dto.Contato;
            await db.SaveChangesAsync();
            return Results.Ok(fab);
        }).WithSummary("Atualiza fabricante");
        g.MapDelete("/fabricantes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.FindAsync(id);
            if (fab is null) return Results.NotFound();
            db.Fabricantes.Remove(fab);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui fabricante");

        g.MapGet("/categorias", async (EstoqueDbContext db) => Results.Ok(await db.Categorias.AsNoTracking().ToListAsync())).WithSummary("Lista categorias");
        g.MapGet("/categorias/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return cat is null ? Results.NotFound() : Results.Ok(cat);
        }).WithSummary("Detalhes da categoria");
        g.MapPost("/categorias", async (CategoriaDto dto, EstoqueDbContext db) =>
        {
            var cat = new Categoria { Nome = dto.Nome, Descricao = dto.Descricao };
            db.Categorias.Add(cat);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/categorias/{cat.Id}", cat);
        }).WithSummary("Cria categoria");
        g.MapPut("/categorias/{id:long}", async (long id, CategoriaDto dto, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return Results.NotFound();
            cat.Nome = dto.Nome;
            cat.Descricao = dto.Descricao;
            await db.SaveChangesAsync();
            return Results.Ok(cat);
        }).WithSummary("Atualiza categoria");
        g.MapDelete("/categorias/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return Results.NotFound();
            db.Categorias.Remove(cat);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui categoria");

        g.MapGet("/localizacoes", async (EstoqueDbContext db) => Results.Ok(await db.Localizacoes.AsNoTracking().ToListAsync())).WithSummary("Lista localizações");
        g.MapGet("/localizacoes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            return loc is null ? Results.NotFound() : Results.Ok(loc);
        }).WithSummary("Detalhes da localização");
        g.MapPost("/localizacoes", async (LocalizacaoDto dto, EstoqueDbContext db) =>
        {
            var loc = new Localizacao { Descricao = dto.Descricao, Corredor = dto.Corredor, Prateleira = dto.Prateleira };
            db.Localizacoes.Add(loc);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/localizacoes/{loc.Id}", loc);
        }).WithSummary("Cria localização");
        g.MapPut("/localizacoes/{id:long}", async (long id, LocalizacaoDto dto, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.FindAsync(id);
            if (loc is null) return Results.NotFound();
            loc.Descricao = dto.Descricao;
            loc.Corredor = dto.Corredor;
            loc.Prateleira = dto.Prateleira;
            await db.SaveChangesAsync();
            return Results.Ok(loc);
        }).WithSummary("Atualiza localização");
        g.MapDelete("/localizacoes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.FindAsync(id);
            if (loc is null) return Results.NotFound();
            db.Localizacoes.Remove(loc);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui localização");

        g.MapPost("/movimentacoes", async (MovimentacaoCreateDto dto, EstoqueDbContext db, IValidator<MovimentacaoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var m = new Movimentacao{ Peca_Id=dto.PecaId, Quantidade=dto.Quantidade, Tipo=dto.Tipo, Referencia=dto.Referencia, Usuario=dto.Usuario };
            db.Movimentacoes.Add(m);
            var peca = await db.Pecas.FindAsync(dto.PecaId);
            if (peca is null) return Results.BadRequest("Peça não encontrada");
            if (dto.Tipo.ToUpper() == "ENTRADA") peca.Quantidade += dto.Quantidade;
            else if (dto.Tipo.ToUpper() == "SAIDA") peca.Quantidade -= dto.Quantidade;
            peca.Touch();
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/movimentacoes/{m.Id}", m);
        }).WithSummary("Lança movimentação de estoque");
    }
}


