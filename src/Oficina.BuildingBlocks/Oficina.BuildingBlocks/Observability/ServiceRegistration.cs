using Microsoft.Extensions.DependencyInjection;

namespace Oficina.BuildingBlocks.Observability;

public static class ServiceRegistration
{
    public static IServiceCollection AddObservability(this IServiceCollection services)
    {
        // Hook para OpenTelemetry/Serilog/NLog se necessÃ¡rio no futuro.
        return services;
    }
}

