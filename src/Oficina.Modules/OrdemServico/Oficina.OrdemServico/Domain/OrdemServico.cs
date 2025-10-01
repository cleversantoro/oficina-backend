using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServico : Entity
{
    public Guid ClienteId { get; set; }
    public Guid MecanicoId { get; set; }
    public string DescricaoProblema { get; set; } = default!;
    public string Status { get; set; } = "ABERTA";
    public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; }
    public List<ItemServico> Itens { get; set; } = new();
}
