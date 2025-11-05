using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class FornecedorContato : Entity
{
    public long Fornecedor_Id { get; set; }
    public Fornecedor Fornecedor { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Valor { get; set; } = default!;
    public bool Principal { get; set; }
    public string? Observacao { get; set; }
}
