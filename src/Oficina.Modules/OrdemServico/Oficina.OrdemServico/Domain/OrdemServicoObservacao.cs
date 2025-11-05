using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoObservacao : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public string? Usuario { get; set; }
 public string Texto { get; set; } = default!;
}
