using System;
using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Endpoints;
using Oficina.Cadastro.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Conn string via env var "ConnectionStrings__OficinaDb" ou "ConnectionStrings__Default"
var connectionString = builder.Configuration.GetConnectionString("OficinaDb")
                      ?? builder.Configuration["ConnectionStrings:OficinaDb"]
                      ?? builder.Configuration.GetConnectionString("Default")
                      ?? builder.Configuration["ConnectionStrings:Default"];

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'OficinaDb' was not found.");
}

builder.Services.AddDbContext<CadastroDbContext>(opt =>
    opt.UseNpgsql(connectionString,
    sql => sql.MigrationsAssembly(typeof(CadastroDbContext).Assembly.FullName))
);

// Cadastro (DI do módulo)
builder.Services.AddCadastroModule(builder.Configuration);

//builder.Services.AddObservability();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new() { Title = "Oficina API", Version = "v1" });
});

// Healthcheck simples
builder.Services.AddHealthChecks();
//.AddNpgSql(connectionString);

// CORS para o Angular
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("default", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true));
});

var app = builder.Build();

app.UseCors("default");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oficina API v1");
    c.RoutePrefix = string.Empty; // deixa o Swagger na raiz "/"
});


app.MapGet("/", () => Results.Ok(new { name = "Oficina API", status = "ok" }));
app.MapGet("/health", () => Results.Ok("healthy"));
app.MapHealthChecks("/healthz");

app.MapCadastroEndpoints();

app.Run();
