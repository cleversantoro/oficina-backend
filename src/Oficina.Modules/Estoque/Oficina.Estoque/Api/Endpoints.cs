using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Api;
using Oficina.Cadastro.Domain;
using Oficina.Cadastro.Infrastructure;
using Oficina.Estoque.Api;
using Oficina.Estoque.Domain;
using Oficina.Estoque.Infrastructure;
using Oficina.SharedKernel.Domain;

namespace Oficina.Estoque;

public static class Endpoints
{
    public static void MapEstoqueEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/estoque").WithTags("Estoque - Peças");
        g.MapGet("/pecas", async (EstoqueDbContext db) =>
        {
            var pecas = await db.Pecas
                .Include(f => f.Fornecedores)
                .Include(f => f.Anexos)
                .Include(f => f.Historicos)
                .AsNoTracking()
                .ToListAsync();
            var result = pecas.Select(MapToPecasDetalhesDto).ToList();
            return Results.Ok(result);
        }).WithSummary("Lista Pecas");
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
        g.MapPost("/pecas", async (PecaCreateDto dto, EstoqueDbContext db, IValidator<PecaCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if (!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var p = new Peca
            {
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
                Fornecedores = dto.Fornecedores?.Select(f => new PecaFornecedor
                {
                    Fornecedor_Id = f.FornecedorId,
                    Preco = f.Preco,
                    Prazo_Entrega = f.PrazoEntrega,
                    Observacao = f.Observacao
                }).ToList() ?? new List<PecaFornecedor>(),
                Anexos = dto.Anexos?.Select(a => new PecaAnexo
                {
                    Nome = a.Nome,
                    Tipo = a.Tipo,
                    Url = a.Url,
                    Observacao = a.Observacao
                }).ToList() ?? new List<PecaAnexo>()
            };
            db.Pecas.Add(p); await db.SaveChangesAsync(); return Results.Created($"/estoque/pecas/{p.Id}", p);
        }).WithSummary("Cria peça");
        g.MapPut("/pecas/{id:long}", async (long id, PecaCreateDto dto, EstoqueDbContext db, IValidator<PecaCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if (!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
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
            peca.Fornecedores = dto.Fornecedores?.Select(f => new PecaFornecedor
            {
                Fornecedor_Id = f.FornecedorId,
                Preco = f.Preco,
                Prazo_Entrega = f.PrazoEntrega,
                Observacao = f.Observacao
            }).ToList() ?? new List<PecaFornecedor>();
            peca.Anexos = dto.Anexos?.Select(a => new PecaAnexo
            {
                Nome = a.Nome,
                Tipo = a.Tipo,
                Url = a.Url,
                Observacao = a.Observacao
            }).ToList() ?? new List<PecaAnexo>();
            await db.SaveChangesAsync();
            return Results.Ok(peca);
        }).WithSummary("Atualiza peça");
        g.MapDelete("/pecas/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var peca = await db.Pecas.FirstOrDefaultAsync(p => p.Id == id);
            if (peca is null) return Results.NotFound();
            db.Pecas.Remove(peca);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui peça");

        var f = app.MapGroup("/estoque").WithTags("Estoque - Fabricantes");
        f.MapGet("/fabricantes", async (EstoqueDbContext db) => Results.Ok(await db.Fabricantes.AsNoTracking().ToListAsync())).WithSummary("Lista fabricantes");
        f.MapGet("/fabricantes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
            return fab is null ? Results.NotFound() : Results.Ok(fab);
        }).WithSummary("Detalhes do fabricante");
        f.MapPost("/fabricantes", async (FabricanteDto dto, EstoqueDbContext db) =>
        {
            var fab = new Fabricante { Nome = dto.Nome, Cnpj = dto.Cnpj, Contato = dto.Contato };
            db.Fabricantes.Add(fab);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/fabricantes/{fab.Id}", fab);
        }).WithSummary("Cria fabricante");
        f.MapPut("/fabricantes/{id:long}", async (long id, FabricanteDto dto, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.FindAsync(id);
            if (fab is null) return Results.NotFound();
            fab.Nome = dto.Nome;
            fab.Cnpj = dto.Cnpj;
            fab.Contato = dto.Contato;
            await db.SaveChangesAsync();
            return Results.Ok(fab);
        }).WithSummary("Atualiza fabricante");
        f.MapDelete("/fabricantes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var fab = await db.Fabricantes.FindAsync(id);
            if (fab is null) return Results.NotFound();
            db.Fabricantes.Remove(fab);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui fabricante");

        var c = app.MapGroup("/estoque").WithTags("Estoque - Categorias");
        c.MapGet("/categorias", async (EstoqueDbContext db) => Results.Ok(await db.Categorias.AsNoTracking().ToListAsync())).WithSummary("Lista categorias");
        c.MapGet("/categorias/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return cat is null ? Results.NotFound() : Results.Ok(cat);
        }).WithSummary("Detalhes da categoria");
        c.MapPost("/categorias", async (CategoriaDto dto, EstoqueDbContext db) =>
        {
            var cat = new Categoria { Nome = dto.Nome, Descricao = dto.Descricao };
            db.Categorias.Add(cat);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/categorias/{cat.Id}", cat);
        }).WithSummary("Cria categoria");
        c.MapPut("/categorias/{id:long}", async (long id, CategoriaDto dto, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return Results.NotFound();
            cat.Nome = dto.Nome;
            cat.Descricao = dto.Descricao;
            await db.SaveChangesAsync();
            return Results.Ok(cat);
        }).WithSummary("Atualiza categoria");
        c.MapDelete("/categorias/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return Results.NotFound();
            db.Categorias.Remove(cat);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui categoria");

        var l = app.MapGroup("/estoque").WithTags("Estoque - Localizações");
        l.MapGet("/localizacoes", async (EstoqueDbContext db) => Results.Ok(await db.Localizacoes.AsNoTracking().ToListAsync())).WithSummary("Lista localizações");
        l.MapGet("/localizacoes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            return loc is null ? Results.NotFound() : Results.Ok(loc);
        }).WithSummary("Detalhes da localização");
        l.MapPost("/localizacoes", async (LocalizacaoDto dto, EstoqueDbContext db) =>
        {
            var loc = new Localizacao { Descricao = dto.Descricao, Corredor = dto.Corredor, Prateleira = dto.Prateleira };
            db.Localizacoes.Add(loc);
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/localizacoes/{loc.Id}", loc);
        }).WithSummary("Cria localização");
        l.MapPut("/localizacoes/{id:long}", async (long id, LocalizacaoDto dto, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.FindAsync(id);
            if (loc is null) return Results.NotFound();
            loc.Descricao = dto.Descricao;
            loc.Corredor = dto.Corredor;
            loc.Prateleira = dto.Prateleira;
            await db.SaveChangesAsync();
            return Results.Ok(loc);
        }).WithSummary("Atualiza localização");
        l.MapDelete("/localizacoes/{id:long}", async (long id, EstoqueDbContext db) =>
        {
            var loc = await db.Localizacoes.FindAsync(id);
            if (loc is null) return Results.NotFound();
            db.Localizacoes.Remove(loc);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui localização");

        var m = app.MapGroup("/estoque").WithTags("Estoque - Movimentações");
        m.MapPost("/movimentacoes", async (MovimentacaoCreateDto dto, EstoqueDbContext db, IValidator<MovimentacaoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto); if (!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var m = new Movimentacao { Peca_Id = dto.Peca_Id, Quantidade = dto.Quantidade, Tipo = dto.Tipo, Referencia = dto.Referencia, Usuario = dto.Usuario };
            db.Movimentacoes.Add(m);
            var peca = await db.Pecas.FindAsync(dto.Peca_Id);
            if (peca is null) return Results.BadRequest("Peça não encontrada");
            if (dto.Tipo.ToUpper() == "ENTRADA") peca.Quantidade += dto.Quantidade;
            else if (dto.Tipo.ToUpper() == "SAIDA") peca.Quantidade -= dto.Quantidade;
            peca.Touch();
            await db.SaveChangesAsync();
            return Results.Created($"/estoque/movimentacoes/{m.Id}", m);
        }).WithSummary("Lança movimentação de estoque");
    }

    private static PecaCreateDto MapToPecasDetalhesDto(Peca peca)
    {
        return new PecaCreateDto(
            peca.Id,
            peca.Codigo,
            peca.Descricao,
            peca.Preco_Unitario,
            peca.Quantidade,
            peca.Estoque_Minimo,
            peca.Estoque_Maximo,
            peca.Unidade,
            peca.Status,
            peca.Observacoes,
            peca.Fabricante_Id,
            peca.Categoria_Id,
            peca.Localizacao_Id,
            peca.Fornecedores.Select(e => new PecaFornecedorDto(
                e.Fornecedor_Id, 
                e.Peca_Id,
                e.Preco, 
                e.Prazo_Entrega, 
                e.Observacao
            )).ToList(),
            peca.Anexos.Select(a => new PecaAnexoDto(
                a.Peca_Id,
                a.Nome,
                a.Tipo,
                a.Url,
                a.Observacao
            )).ToList(),
            peca.Historicos.Select(h => new PecaHistoricoDto(
                h.Peca_Id,
                h.Data_Alteracao,
                h.Usuario,
                h.Campo,
                h.Valor_Antigo,
                h.Valor_Novo
            )).ToList()
        );
    }
}


