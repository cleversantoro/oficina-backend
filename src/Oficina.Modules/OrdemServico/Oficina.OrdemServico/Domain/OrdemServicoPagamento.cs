using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoPagamento : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public decimal Valor { get; set; }
 public string Status { get; set; } = default!;
 public DateTime? Data_Pagamento { get; set; }
 public string? Metodo { get; set; }
 public string? Observacao { get; set; }
}
