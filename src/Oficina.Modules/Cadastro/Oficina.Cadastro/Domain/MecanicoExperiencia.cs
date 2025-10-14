using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoExperiencia : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public string Empresa { get; set; } = default!;
    public string Cargo { get; set; } = default!;
    public DateTime? Data_Inicio { get; set; }
    public DateTime? Data_Fim { get; set; }
    public string? Resumo_Atividades { get; set; }
}
