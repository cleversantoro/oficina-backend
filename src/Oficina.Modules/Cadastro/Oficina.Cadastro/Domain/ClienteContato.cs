using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteContato : Entity
{
    public Guid Cliente_Id { get; set; }
    public ClienteContatoTipo Tipo { get; set; }
    public string Valor { get; set; } = default!;
    public bool Principal { get; set; }
    public string? Observacao { get; set; }
}
