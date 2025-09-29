using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

namespace Oficina.Observability;

public static class ObservabilitySetup
{
    public static void AddObservability(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithTracing(builder =>
            {
                builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Oficina.Cliente"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource("Oficina.Messaging.ClientMessagingService")
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("http://localhost:4317"); // OTLP endpoint do Jaeger
                    });
            });
    }
}