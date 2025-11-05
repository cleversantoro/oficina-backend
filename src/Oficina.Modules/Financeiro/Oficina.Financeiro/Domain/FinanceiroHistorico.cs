using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class FinanceiroHistorico : Entity
{
 public string Entidade { get; set; } = default!;
 public long Entidade_Id { get; set; }
 public DateTime Data_Alteracao { get; set; } = DateTime.UtcNow;
 public string? Usuario { get; set; }
 public string? Campo { get; set; }
 public string? Valor_Antigo { get; set; }
 public string? Valor_Novo { get; set; }
}
