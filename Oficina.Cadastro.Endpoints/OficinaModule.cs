using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Oficina.Cadastro.Infrastructure.Persistence;
using Oficina.Cadastro.Infrastructure.Repositories;
using Oficina.Cadastro.Application.Services;
using Oficina.Cadastro.Application.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Oficina.Cadastro.Endpoints;

public static class OficinaModule
{
    public static IServiceCollection AddCadastroModule(this IServiceCollection services, IConfiguration config)
    {
        // Usa a ConnectionString específica do módulo (OficinaDb), com fallback para a padrão
        var cs =
            config.GetConnectionString("OficinaDb") ??
            config["ConnectionStrings:OficinaDb"] ??
            config.GetConnectionString("Default") ??
            config["ConnectionStrings:Default"];

        if (string.IsNullOrWhiteSpace(cs))
        {
            throw new InvalidOperationException("Connection string 'OficinaDb' was not found.");
        }
        services.AddDbContext<CadastroDbContext>(opt => 
        opt.UseNpgsql(cs, x => x.MigrationsHistoryTable("__EFMigrationsHistory", CadastroDbContext.Schema)));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IMotoRepository, MotoRepository>(); // (implemente igual ao Cliente)
        services.AddScoped<IClienteAppService, ClienteAppService>();
        services.AddScoped<IMotoAppService, MotoAppService>();

        services.AddAutoMapper(typeof(Oficina.Cadastro.Application.Mapping.CadastroProfile).Assembly);

        return services;
    }

    public static IEndpointRouteBuilder MapCadastroEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/api/cadastro").WithTags("Cadastro");

        // === Clientes ===
        g.MapGet("/clientes", async (IClienteAppService svc, string? q, int page = 1, int pageSize = 20, CancellationToken ct = default)
            => Results.Ok(await svc.SearchAsync(q, page, pageSize, ct)));

        g.MapGet("/clientes/{id:guid}", async (IClienteAppService svc, Guid id, CancellationToken ct = default) =>
        {
            var dto = await svc.GetAsync(id, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        g.MapPost("/clientes", async (IClienteAppService svc, CreateClienteRequest req, CancellationToken ct = default) =>
        {
            var id = await svc.CreateAsync(req, ct);
            return Results.Created($"/api/cadastro/clientes/{id}", new { id });
        });

        g.MapPut("/clientes/{id:guid}", async (IClienteAppService svc, Guid id, UpdateClienteRequest req, CancellationToken ct = default) =>
        {
            await svc.UpdateAsync(id, req, ct);
            return Results.NoContent();
        });

        g.MapDelete("/clientes/{id:guid}", async (IClienteAppService svc, Guid id, CancellationToken ct = default) =>
        {
            await svc.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        // === Motos === (similar)
        g.MapGet("/motos", async (IMotoAppService svc, Guid? clienteId, string? placa, int page = 1, int pageSize = 20, CancellationToken ct = default)
            => Results.Ok(await svc.SearchAsync(clienteId, placa, page, pageSize, ct)));

        g.MapPost("/motos", async (IMotoAppService svc, CreateMotoRequest req, CancellationToken ct = default) =>
        {
            var id = await svc.CreateAsync(req, ct);
            return Results.Created($"/api/cadastro/motos/{id}", new { id });
        });

        g.MapGet("/motos/{id:guid}", async (IMotoAppService svc, Guid id, CancellationToken ct = default) =>
        {
            var dto = await svc.GetAsync(id, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        g.MapPut("/motos/{id:guid}", async (IMotoAppService svc, Guid id, UpdateMotoRequest req, CancellationToken ct = default) =>
        {
            await svc.UpdateAsync(id, req, ct);
            return Results.NoContent();
        });

        g.MapDelete("/motos/{id:guid}", async (IMotoAppService svc, Guid id, CancellationToken ct = default) =>
        {
            await svc.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
