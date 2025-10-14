using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoEspecialidadeRel : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public long Especialidade_Id { get; set; }
    public MecanicoEspecialidade Especialidade { get; set; } = default!;
    public string Nivel { get; set; } = "Pleno";
    public bool Principal { get; set; }
    public string? Anotacoes { get; set; }
}
