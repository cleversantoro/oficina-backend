using Oficina.SharedKernel.Domain;
namespace Oficina.Cadastro.Domain;
public class Fornecedor : Entity
{
    public string Razao_Social { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string Contato { get; set; } = default!;
}

