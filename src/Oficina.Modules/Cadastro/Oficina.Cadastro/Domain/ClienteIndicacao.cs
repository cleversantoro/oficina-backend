using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteIndicacao : Entity
{
    public long ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Indicador_Nome { get; set; } = default!;
    public string? Indicador_Telefone { get; set; }
    public string? Observacao { get; set; }
}

