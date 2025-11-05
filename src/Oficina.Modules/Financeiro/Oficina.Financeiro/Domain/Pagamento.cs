using Oficina.SharedKernel.Domain;
namespace Oficina.Financeiro.Domain;
public class Pagamento : Entity
{
    public long Ordem_Servico_Id { get; set; }
    public string Meio { get; set; } = default!;
    public decimal Valor { get; set; }
    public string Status { get; set; } = "PENDENTE";
    public string? Transacao_Id { get; set; }
    public long? Cliente_Id { get; set; }
    public long? Fornecedor_Id { get; set; }
    public long? Metodo_Id { get; set; }
    public string? Observacao { get; set; }
    public DateTime? Data_Pagamento { get; set; }
}


