using Oficina.SharedKernel.Domain;
using System.Collections.Generic;
namespace Oficina.OrdemServico.Domain;
public class OrdemServico : Entity
{
    public long Cliente_Id { get; set; }
    public long Mecanico_Id { get; set; }
    public string Descricao_Problema { get; set; } = default!;
    public string Status { get; set; } = "ABERTA";
    public DateTime Data_Abertura { get; set; } = DateTime.UtcNow;
    public DateTime? Data_Conclusao { get; set; }
    public List<ItemServico> Itens { get; set; } = new();
    public List<OrdemServicoAnexo> Anexos { get; set; } = new();
    public List<OrdemServicoHistorico> Historicos { get; set; } = new();
    public List<OrdemServicoChecklist> Checklists { get; set; } = new();
    public List<OrdemServicoAvaliacao> Avaliacoes { get; set; } = new();
    public List<OrdemServicoPagamento> Pagamentos { get; set; } = new();
    public List<OrdemServicoObservacao> Observacoes { get; set; } = new();
}


