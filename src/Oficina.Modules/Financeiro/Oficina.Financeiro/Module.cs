using Microsoft.Extensions.DependencyInjection;

namespace Oficina.Financeiro;

public static class FinanceiroModule
{
    public static IServiceCollection AddFinanceiroModule(this IServiceCollection services)
    {
        services.AddScoped<Infrastructure.FinanceiroDbContext>();
        return services;
    }
}

