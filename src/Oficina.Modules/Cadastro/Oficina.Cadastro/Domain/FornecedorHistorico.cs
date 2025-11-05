using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class FornecedorHistorico : Entity
{
public long Fornecedor_Id { get; set; }
public Fornecedor Fornecedor { get; set; } = default!;
public DateTime Data_Alteracao { get; set; } = DateTime.UtcNow;
    public string Usuario { get; set; } = default!;
    public string Campo { get; set; } = default!;
    public string? Valor_Antigo { get; set; }
    public string? Valor_Novo { get; set; }
}
