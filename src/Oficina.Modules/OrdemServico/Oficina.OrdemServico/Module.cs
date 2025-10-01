using Microsoft.Extensions.DependencyInjection;

namespace Oficina.OrdemServico;

public static class OrdemServicoModule
{
    public static IServiceCollection AddOrdemServicoModule(this IServiceCollection services)
    {
        services.AddScoped<Infrastructure.OrdemServicoDbContext>();
        return services;
    }
}
