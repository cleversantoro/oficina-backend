using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteLgpdConsentimento : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public bool Aceito { get; set; }
    public DateTime Data_Consentimento { get; set; }
    public string Canal { get; set; } = default!;
    public string? Observacao { get; set; }
}
