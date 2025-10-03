using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Infrastructure;
using Oficina.Cadastro.Domain;
using Oficina.Cadastro.Api;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Oficina.Cadastro;

public static class Endpoints
{
    public static void MapCadastroEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/cadastro").WithTags("Cadastro");

        g.MapGet("/clientes", async (CadastroDbContext db) => Results.Ok(await db.Clientes.AsNoTracking().ToListAsync())).WithSummary("Lista clientes");
        
        g.MapGet("/clientes/{id:long}", async (long id, CadastroDbContext db) =>
            await db.Clientes.FindAsync(id) is { } c ? Results.Ok(c) : Results.NotFound()).WithSummary("Obtém cliente por Id");
        
        g.MapPost("/clientes", async (ClienteCreateDto dto, CadastroDbContext db, IValidator<ClienteCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var c = new Cliente{ Nome=dto.Nome, Documento=dto.Documento, Telefone=dto.Telefone, Email=dto.Email };
            db.Clientes.Add(c); await db.SaveChangesAsync(); return Results.Created($"/cadastro/clientes/{c.Id}", c);
        }).WithSummary("Cria cliente");
        
        g.MapPut("/clientes/{id:long}", async (long id, ClienteCreateDto dto, CadastroDbContext db, IValidator<ClienteCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var c = await db.Clientes.FindAsync(id); if (c is null) return Results.NotFound();
            c.Nome = dto.Nome; c.Documento = dto.Documento; c.Telefone = dto.Telefone; c.Email = dto.Email; c.Touch();
            await db.SaveChangesAsync(); return Results.Ok(c);
        }).WithSummary("Atualiza cliente");
        
        g.MapDelete("/clientes/{id:long}", async (long id, CadastroDbContext db) => {
            var c = await db.Clientes.FindAsync(id); if (c is null) return Results.NotFound();
            db.Remove(c); await db.SaveChangesAsync(); return Results.NoContent();
        }).WithSummary("Exclui cliente");

        g.MapGet("/mecanicos", async (CadastroDbContext db) => Results.Ok(await db.Mecanicos.AsNoTracking().ToListAsync())).WithSummary("Lista mecânicos");
        g.MapPost("/mecanicos", async (MecanicoCreateDto dto, CadastroDbContext db, IValidator<MecanicoCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var m = new Mecanico{ Nome=dto.Nome, Especialidade=dto.Especialidade ?? "Geral" };
            db.Mecanicos.Add(m); await db.SaveChangesAsync(); return Results.Created($"/cadastro/mecanicos/{m.Id}", m);
        }).WithSummary("Cria mecânico");

        g.MapGet("/fornecedores", async (CadastroDbContext db) => Results.Ok(await db.Fornecedores.AsNoTracking().ToListAsync())).WithSummary("Lista fornecedores");
        g.MapPost("/fornecedores", async (FornecedorCreateDto dto, CadastroDbContext db, IValidator<FornecedorCreateDto> v) => {
            var vr = await v.ValidateAsync(dto); if(!vr.IsValid) return Results.ValidationProblem(vr.ToDictionary());
            var f = new Fornecedor{ Razao_Social=dto.RazaoSocial, Cnpj=dto.Cnpj, Contato=dto.Contato };
            db.Fornecedores.Add(f); await db.SaveChangesAsync(); return Results.Created($"/cadastro/fornecedores/{f.Id}", f);
        }).WithSummary("Cria fornecedor");
    }
}
