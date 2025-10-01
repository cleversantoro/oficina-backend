using Microsoft.Extensions.DependencyInjection;

namespace Oficina.Estoque;

public static class EstoqueModule
{
    public static IServiceCollection AddEstoqueModule(this IServiceCollection services)
    {
        services.AddScoped<Infrastructure.EstoqueDbContext>();
        return services;
    }
}
