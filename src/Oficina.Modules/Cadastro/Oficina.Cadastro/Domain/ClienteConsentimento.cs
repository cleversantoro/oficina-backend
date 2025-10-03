using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteConsentimento : Entity
{
    public Guid Cliente_Id { get; set; }
    public ClienteConsentimentoTipo Tipo { get; set; }
    public bool Aceito { get; set; }
    public DateTime? Data { get; set; }
    public DateTime? Valido_Ate { get; set; }
    public string? Observacoes { get; set; }
}
