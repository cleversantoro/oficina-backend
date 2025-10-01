using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class ItemServico : Entity
{
    public Guid OrdemServicoId { get; set; }
    public Guid? PecaId { get; set; }
    public string Descricao { get; set; } = default!;
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Total => Math.Round(Quantidade * ValorUnitario, 2);
}
