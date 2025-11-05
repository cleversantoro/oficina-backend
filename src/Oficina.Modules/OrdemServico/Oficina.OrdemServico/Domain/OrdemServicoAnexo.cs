using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoAnexo : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public string? Nome { get; set; }
 public string? Tipo { get; set; }
 public string? Url { get; set; }
 public string? Observacao { get; set; }
 public DateTime Data_Upload { get; set; } = DateTime.UtcNow;
}
