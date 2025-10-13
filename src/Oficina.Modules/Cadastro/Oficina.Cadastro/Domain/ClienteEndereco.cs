using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteEndereco : Entity
{
    public long Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public ClienteEnderecoTipo Tipo { get; set; }
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Cep { get; set; } = default!;
    public string Pais { get; set; } = string.Empty;
    public bool Principal { get; set; }
}
