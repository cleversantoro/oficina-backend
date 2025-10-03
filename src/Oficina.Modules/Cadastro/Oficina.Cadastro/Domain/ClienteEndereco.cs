using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteEndereco : Entity
{
    public Guid Cliente_Id { get; set; }
    public ClienteEnderecoTipo Tipo { get; set; }
    public string Cep { get; set; } = default!;
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Pais { get; set; } = "BR";
    public string? Complemento { get; set; }
    public bool Principal { get; set; }
}
