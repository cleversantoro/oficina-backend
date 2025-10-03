using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteOrigem : Entity
{
    public string Nome { get; set; } = default!;
    public string? Descricao { get; set; }
    public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
