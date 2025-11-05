using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class FinanceiroAnexo : Entity
{
 public long? Pagamento_Id { get; set; }
 public long? Conta_Pagar_Id { get; set; }
 public long? Conta_Receber_Id { get; set; }
 public string? Nome { get; set; }
 public string? Tipo { get; set; }
 public string? Url { get; set; }
 public string? Observacao { get; set; }
 public DateTime Data_Upload { get; set; } = DateTime.UtcNow;
}
