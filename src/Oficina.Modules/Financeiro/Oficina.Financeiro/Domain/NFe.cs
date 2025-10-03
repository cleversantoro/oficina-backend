using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class NFe : Entity
{
    public long Ordem_Servico_Id { get; set; }
    public string Numero { get; set; } = default!;
    public string Chave_Acesso { get; set; } = default!;
    public string Status { get; set; } = "EMITIDA";
}
