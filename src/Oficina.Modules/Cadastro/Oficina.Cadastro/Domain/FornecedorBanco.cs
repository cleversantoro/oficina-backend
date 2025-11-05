using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class FornecedorBanco : Entity
{
    public long Fornecedor_Id { get; set; }
    public Fornecedor Fornecedor { get; set; } = default!;
    public string Banco { get; set; } = default!;
    public string Agencia { get; set; } = default!;
    public string Conta { get; set; } = default!;
    public string Tipo_Conta { get; set; } = default!;
    public string Titular { get; set; } = default!;
    public string Documento_Titular { get; set; } = default!;
    public string? Pix_Chave { get; set; }
}
