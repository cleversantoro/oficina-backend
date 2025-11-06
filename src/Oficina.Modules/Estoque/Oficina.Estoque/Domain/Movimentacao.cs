using Oficina.SharedKernel.Domain;
namespace Oficina.Estoque.Domain;
public class Movimentacao : Entity
{
    public long Peca_Id { get; set; }
    public Peca? Peca { get; set; }
    public int Quantidade { get; set; }
    public string Tipo { get; set; } = default!;
    public string? Referencia { get; set; }
    public DateTime Data_Movimentacao { get; set; } = DateTime.UtcNow;
    public string? Usuario { get; set; }
}


