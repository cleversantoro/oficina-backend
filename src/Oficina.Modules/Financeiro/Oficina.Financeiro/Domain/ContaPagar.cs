using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class ContaPagar : Entity
{
 public long? Fornecedor_Id { get; set; }
 public string Descricao { get; set; } = default!;
 public decimal Valor { get; set; }
 public DateTime Vencimento { get; set; }
 public string Status { get; set; } = default!;
 public DateTime? Data_Pagamento { get; set; }
 public long? Metodo_Id { get; set; }
 public string? Observacao { get; set; }
}
