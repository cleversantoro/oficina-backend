using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteConsentimento : Entity
{
    public long Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public ClienteConsentimentoTipo Tipo { get; set; }
    public bool Aceito { get; set; }
    public DateTime? Data { get; set; }
    public DateTime? Valido_Ate { get; set; }
    public string? Observacoes { get; set; }
    public string Canal { get; set; } = default!;
}
