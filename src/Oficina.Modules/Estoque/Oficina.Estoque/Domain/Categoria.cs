using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Categoria : Entity
{
    public string Nome { get; set; } = default!;
    public string? Descricao { get; set; }
}
