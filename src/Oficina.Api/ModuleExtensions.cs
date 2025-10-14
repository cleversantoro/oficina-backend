using Microsoft.Extensions.DependencyInjection;

namespace Oficina.Cadastro { public static class CadastroExt { public static IServiceCollection AddCadastroEndpoints(this IServiceCollection s)=>s; } }
namespace Oficina.OrdemServico { public static class OrdemExt { public static IServiceCollection AddOrdemServicoEndpoints(this IServiceCollection s)=>s; } }
namespace Oficina.Estoque { public static class EstoqueExt { public static IServiceCollection AddEstoqueEndpoints(this IServiceCollection s)=>s; } }
namespace Oficina.Financeiro { public static class FinanceiroExt { public static IServiceCollection AddFinanceiroEndpoints(this IServiceCollection s)=>s; } }

