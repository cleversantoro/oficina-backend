using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class FornecedorAnexo : Entity
{
    public long Fornecedor_Id { get; set; }
    public Fornecedor Fornecedor { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string? Observacao { get; set; }
    public DateTime Data_Upload { get; set; } = DateTime.UtcNow;
}
