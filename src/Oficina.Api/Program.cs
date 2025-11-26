using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using Serilog;

using Oficina.Cadastro.Infrastructure;
using Oficina.OrdemServico.Infrastructure;
using Oficina.Estoque.Infrastructure;
using Oficina.Financeiro.Infrastructure;

using Oficina.Cadastro;
using Oficina.OrdemServico;
using Oficina.Estoque;
using Oficina.Financeiro;
using Oficina.Financeiro.Infrastructure.External;
using Oficina.BuildingBlocks.Observability;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Serilog bootstrap
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console());

var conn = builder.Configuration.GetConnectionString("OficinaDb")
    ?? "server=localhost;port=3306;database=oficina_db;user=oficina;password=Oficina123@@";

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

// Observability: basic tracing via Serilog and OpenTelemetry metrics with Prometheus exporter
builder.Services.AddObservability("Oficina.Api");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Oficina.Api"))
    .WithTracing(builderTracing =>
    {
        builderTracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        var otlpEndpoint = builder.Configuration["Observability:Otlp:Endpoint"];
        if (!string.IsNullOrWhiteSpace(otlpEndpoint))
        {
            builderTracing.AddOtlpExporter(o => o.Endpoint = new Uri(otlpEndpoint));
        }
    })
    .WithMetrics(builderMetric =>
    {
        builderMetric
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        builderMetric.AddPrometheusExporter();

        var otlpMetrics = builder.Configuration["Observability:Otlp:MetricsEndpoint"];
        if (!string.IsNullOrWhiteSpace(otlpMetrics))
        {
            builderMetric.AddOtlpExporter(o => o.Endpoint = new Uri(otlpMetrics));
        }
    });

// Swagger & CORS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => { o.SwaggerDoc("v1", new() { Title = "Oficina API", Version = "v1" }); });
builder.Services.AddCors(opt => opt.AddPolicy("default", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors("default");
app.UseSwagger();
app.UseSwaggerUI(c => c.DocumentTitle = "Oficina API");
app.MapGet("/health", () => Results.Ok(new { status = "ok", ts = DateTime.UtcNow })).WithTags("*Health*").WithSummary("Verifica se a API esta online.");

app.UseHttpMetrics();
app.MapMetrics("/metrics");

// Map Prometheus scraping endpoint
app.UseOpenTelemetryPrometheusScrapingEndpoint();

// Endpoints
app.MapCadastroEndpoints();
app.MapOrdemServicoEndpoints();
app.MapEstoqueEndpoints();
app.MapFinanceiroEndpoints();

app.Run();


