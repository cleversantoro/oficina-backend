using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Fabricante : Entity
{
    public string Nome { get; set; } = default!;
    public string? Cnpj { get; set; }
    public string? Contato { get; set; }
}
