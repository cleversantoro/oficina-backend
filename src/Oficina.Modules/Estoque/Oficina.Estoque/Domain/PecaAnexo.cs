using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class PecaAnexo : Entity
{
    public long Peca_Id { get; set; }
    public Peca Peca { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string? Observacao { get; set; }
    public DateTime Data_Upload { get; set; } = DateTime.UtcNow;
}
