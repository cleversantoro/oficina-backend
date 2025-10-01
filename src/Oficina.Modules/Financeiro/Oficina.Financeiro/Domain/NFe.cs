using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class NFe : Entity
{
    public Guid OrdemServicoId { get; set; }
    public string Numero { get; set; } = default!;
    public string ChaveAcesso { get; set; } = default!;
    public string Status { get; set; } = "EMITIDA";
}
