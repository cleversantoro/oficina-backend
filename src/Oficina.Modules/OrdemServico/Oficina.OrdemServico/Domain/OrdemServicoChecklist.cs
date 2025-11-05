using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoChecklist : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public string Item { get; set; } = default!;
 public bool Realizado { get; set; }
 public string? Observacao { get; set; }
}
