using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class PessoaPf : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string? Rg { get; set; }
    public DateTime? Data_Nascimento { get; set; }
    public string? Genero { get; set; }
    public string? Estado_Civil { get; set; }
    public string? Profissao { get; set; }
}
