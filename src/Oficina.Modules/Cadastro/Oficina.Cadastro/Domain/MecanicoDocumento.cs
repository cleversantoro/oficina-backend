using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoDocumento : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public DateTime? Data_Emissao { get; set; }
    public DateTime? Data_Validade { get; set; }
    public string? Orgao_Expedidor { get; set; }
    public string? Arquivo_Url { get; set; }
}
