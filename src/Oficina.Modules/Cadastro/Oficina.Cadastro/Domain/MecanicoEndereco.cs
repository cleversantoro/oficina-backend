using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoEndereco : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Cep { get; set; } = default!;
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string? Pais { get; set; }
    public string? Complemento { get; set; }
    public bool Principal { get; set; }
}
