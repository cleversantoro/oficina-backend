using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Peca : Entity
{
    public string Codigo { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public Guid? FornecedorId { get; set; }
}
