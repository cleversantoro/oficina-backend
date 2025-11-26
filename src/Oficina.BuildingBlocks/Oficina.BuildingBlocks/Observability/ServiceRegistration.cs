using Microsoft.Extensions.DependencyInjection;

namespace Oficina.BuildingBlocks.Observability;

public static class ServiceRegistration
{
    public static IServiceCollection AddObservability(this IServiceCollection services, string serviceName = "Oficina.Api")
    {
        // Keep this extension as a registration point for future observability helpers.
        // Instrumentation and exporters are configured at the host (API) level.
        return services;
    }
}


