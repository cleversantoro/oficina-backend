using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoCertificacao : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public long? Especialidade_Id { get; set; }
    public MecanicoEspecialidade? Especialidade { get; set; }
    public string Titulo { get; set; } = default!;
    public string? Instituicao { get; set; }
    public DateTime? Data_Conclusao { get; set; }
    public DateTime? Data_Validade { get; set; }
    public string? Codigo_Certificacao { get; set; }
}
