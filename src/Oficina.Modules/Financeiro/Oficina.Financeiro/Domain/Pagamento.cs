using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class Pagamento : Entity
{
    public Guid OrdemServicoId { get; set; }
    public string Meio { get; set; } = default!;
    public decimal Valor { get; set; }
    public string Status { get; set; } = "PENDENTE";
    public string? TransacaoId { get; set; }
}
