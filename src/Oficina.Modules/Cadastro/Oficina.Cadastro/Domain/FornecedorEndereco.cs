using Oficina.SharedKernel.Domain;
using System.Collections.Generic;

namespace Oficina.Cadastro.Domain;

public class FornecedorEndereco : Entity
{
    public long Fornecedor_Id { get; set; }
    public Fornecedor Fornecedor { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Cep { get; set; } = default!;
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Pais { get; set; } = default!;
    public string? Complemento { get; set; }
    public bool Principal { get; set; }
}
