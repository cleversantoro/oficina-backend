using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Localizacao : Entity
{
    public string Descricao { get; set; } = default!;
    public string? Corredor { get; set; }
    public string? Prateleira { get; set; }
}
