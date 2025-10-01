using Microsoft.Extensions.DependencyInjection;

namespace Oficina.Cadastro;

public static class CadastroModule
{
    public static IServiceCollection AddCadastroModule(this IServiceCollection services)
    {
        services.AddScoped<Infrastructure.CadastroDbContext>();
        return services;
    }
}
