using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteAnexo : Entity
{
    public long Cliente_Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string? Observacao { get; set; }
}
