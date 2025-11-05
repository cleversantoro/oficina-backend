using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class ContaReceber : Entity
{
 public long? Cliente_Id { get; set; }
 public string Descricao { get; set; } = default!;
 public decimal Valor { get; set; }
 public DateTime Vencimento { get; set; }
 public string Status { get; set; } = default!;
 public DateTime? Data_Recebimento { get; set; }
 public long? Metodo_Id { get; set; }
 public string? Observacao { get; set; }
}
