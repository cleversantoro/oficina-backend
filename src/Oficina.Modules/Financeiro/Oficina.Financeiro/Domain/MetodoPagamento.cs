using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class MetodoPagamento : Entity
{
 public string Nome { get; set; } = default!;
 public string? Descricao { get; set; }
}
