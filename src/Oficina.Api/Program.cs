using FluentValidation;
using Microsoft.EntityFrameworkCore;

using Oficina.Cadastro.Infrastructure;
using Oficina.OrdemServico.Infrastructure;
using Oficina.Estoque.Infrastructure;
using Oficina.Financeiro.Infrastructure;

using Oficina.Cadastro;
using Oficina.OrdemServico;
using Oficina.Estoque;
using Oficina.Financeiro;
using Oficina.Financeiro.Infrastructure.External;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("OficinaDb")
    ?? "server=localhost;port=3306;database=oficina_db;user=oficina;password=oficina123";

// DbContexts (mesmo DB para todos os BCs)
builder.Services.AddDbContext<CadastroDbContext>(opt => opt.UseMySql(conn, ServerVersion.AutoDetect(conn)));
builder.Services.AddDbContext<OrdemServicoDbContext>(opt => opt.UseMySql(conn, ServerVersion.AutoDetect(conn)));
builder.Services.AddDbContext<EstoqueDbContext>(opt => opt.UseMySql(conn, ServerVersion.AutoDetect(conn)));
builder.Services.AddDbContext<FinanceiroDbContext>(opt => opt.UseMySql(conn, ServerVersion.AutoDetect(conn)));

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<Oficina.Cadastro.Api.ClienteCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Oficina.OrdemServico.Api.OrdemCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Oficina.Estoque.Api.PecaCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Oficina.Financeiro.Api.PagamentoCreateValidator>();

// External clients (mocks)
builder.Services.AddScoped<IPaymentGatewayClient, FakePaymentGatewayClient>();
builder.Services.AddScoped<INfeClient, FakeNfeClient>();

// Swagger & CORS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => { o.SwaggerDoc("v1", new() { Title = "Oficina API", Version = "v1" }); });
builder.Services.AddCors(opt => opt.AddPolicy("default", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors("default");
app.UseSwagger();
app.UseSwaggerUI(c => c.DocumentTitle = "Oficina API");
app.MapGet("/health", () => Results.Ok(new { status = "ok", ts = DateTime.UtcNow }));

// Endpoints
app.MapCadastroEndpoints();
app.MapOrdemServicoEndpoints();
app.MapEstoqueEndpoints();
app.MapFinanceiroEndpoints();

app.Run();
