using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteContato : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Tipo { get; set; } = "Telefone";
    public string Valor { get; set; } = default!;
    public string? Observacao { get; set; }
    public bool Principal { get; set; }
}
