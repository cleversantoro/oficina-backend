using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class ItemServico : Entity
{
    public Guid Ordem_Servico_Id { get; set; }
    public Guid? Peca_Id { get; set; }
    public string Descricao { get; set; } = default!;
    public int Quantidade { get; set; }
    public decimal Valor_Unitario { get; set; }
    public decimal Total => Math.Round(Quantidade * Valor_Unitario, 2);
}
