using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoHistorico : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public DateTime Data_Alteracao { get; set; } = DateTime.UtcNow;
 public string? Usuario { get; set; }
 public string? Campo { get; set; }
 public string? Valor_Antigo { get; set; }
 public string? Valor_Novo { get; set; }
}
