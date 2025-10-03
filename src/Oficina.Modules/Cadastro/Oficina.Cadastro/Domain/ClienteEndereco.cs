using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteEndereco : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Tipo { get; set; } = "Residencial";
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Cep { get; set; } = default!;
    public string? Pais { get; set; }
    public bool Principal { get; set; }
}
