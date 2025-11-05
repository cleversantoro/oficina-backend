using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class Lancamento : Entity
{
 public string Tipo { get; set; } = default!; // ENTRADA ou SAIDA
 public string Descricao { get; set; } = default!;
 public decimal Valor { get; set; }
 public DateTime Data_Lancamento { get; set; } = DateTime.UtcNow;
 public string? Referencia { get; set; }
 public string? Observacao { get; set; }
}
