using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteDocumento : Entity
{
    public long Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Documento { get; set; } = default!;
    public DateTime? Data_Emissao { get; set; }
    public DateTime? Data_Validade { get; set; }
    public string? Orgao_Expedidor { get; set; }
    public bool Principal { get; set; }
}
