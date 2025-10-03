using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Peca : Entity
{
    public string Codigo { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public decimal Preco_Unitario { get; set; }
    public int Quantidade { get; set; }
    public long? Fornecedor_Id { get; set; }
}
