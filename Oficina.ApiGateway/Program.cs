using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Endpoints;
using Oficina.Cadastro.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Conn string via env var "ConnectionStrings__Default"
var connectionString = builder.Configuration.GetConnectionString("OficinaDb") ?? builder.Configuration["ConnectionStrings:OficinaDb"];
builder.Services.AddDbContext<CadastroDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddDbContext<CadastroDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("OficinaDb"),
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new { name = "Oficina API", status = "ok" }));
app.MapGet("/health", () => Results.Ok("healthy"));
app.MapHealthChecks("/healthz");

app.MapCadastroEndpoints();

app.Run();
