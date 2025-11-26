Observability setup for Oficina API

This project exposes OpenTelemetry metrics and traces. Prometheus scraping endpoint is available at `/metrics`.

How to use

1. Ensure Prometheus is configured to scrape `http://<api-host>:<port>/metrics`.
2. For traces, configure an OpenTelemetry collector and point exporters (OTLP/Jaeger) there.

Local quick test

- Run the API: `dotnet run --project src/Oficina.Api/Oficina.Api.csproj`
- Metrics endpoint: `http://localhost:5134/metrics` (port may vary)
- Health: `http://localhost:5134/health`

Notes

- Building blocks expose tracing (instrumentation) only. The API project configures the Prometheus exporter to expose metrics.
- For production, add an OTLP exporter in `Program.cs` and/or route to a collector. Also integrate structured logging (Serilog) and adjust sampling rates.
