using Oficina.SharedKernel.Domain;
namespace Oficina.OrdemServico.Domain;
public class OrdemServicoAvaliacao : Entity
{
 public long Ordem_Servico_Id { get; set; }
 public int Nota { get; set; }
 public string? Comentario { get; set; }
 public string? Usuario { get; set; }
}
