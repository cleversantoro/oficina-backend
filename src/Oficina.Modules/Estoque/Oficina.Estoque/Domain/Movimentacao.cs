using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Movimentacao : Entity
{
    public Guid PecaId { get; set; }
    public int Quantidade { get; set; }
    public string Tipo { get; set; } = default!;
    public string? Referencia { get; set; }
}
