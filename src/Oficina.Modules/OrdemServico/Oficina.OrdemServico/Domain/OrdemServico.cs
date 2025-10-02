using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServico : Entity
{
    public Guid Cliente_Id { get; set; }
    public Guid Mecanico_Id { get; set; }
    public string Descricao_Problema { get; set; } = default!;
    public string Status { get; set; } = "ABERTA";
    public DateTime Data_Abertura { get; set; } = DateTime.UtcNow;
    public DateTime? Data_Conclusao { get; set; }
    public List<ItemServico> Itens { get; set; } = new();
}
