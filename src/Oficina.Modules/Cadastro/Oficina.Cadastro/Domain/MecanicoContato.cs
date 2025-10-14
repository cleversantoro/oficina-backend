using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoContato : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Valor { get; set; } = default!;
    public bool Principal { get; set; }
    public string? Observacao { get; set; }
}
