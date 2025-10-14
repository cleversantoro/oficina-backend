using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteFinanceiro : Entity
{
    public long ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public decimal Limite_Credito { get; set; }
    public int? Prazo_Pagamento { get; set; }
    public bool Bloqueado { get; set; }
    public string? Observacoes { get; set; }
}

