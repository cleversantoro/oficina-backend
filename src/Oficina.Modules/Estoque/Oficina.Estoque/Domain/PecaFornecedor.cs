using Oficina.Cadastro.Domain;
using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class PecaFornecedor : Entity
{
    public long Peca_Id { get; set; }
    public Peca Peca { get; set; } = default!;
    public long Fornecedor_Id { get; set; }
    public Fornecedor Fornecedor { get; set; } = default!;
    public decimal? Preco { get; set; }
    public int? Prazo_Entrega { get; set; }
    public string? Observacao { get; set; }
}
