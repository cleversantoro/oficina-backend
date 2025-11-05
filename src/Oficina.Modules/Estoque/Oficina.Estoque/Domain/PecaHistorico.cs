using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class PecaHistorico : Entity
{
    public long Peca_Id { get; set; }
    public DateTime Data_Alteracao { get; set; } = DateTime.UtcNow;
public string Usuario { get; set; } = default!;
    public string Campo { get; set; } = default!;
    public string? Valor_Antigo { get; set; }
    public string? Valor_Novo { get; set; }
}
