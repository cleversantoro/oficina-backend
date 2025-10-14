using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClientePessoaFisica : Entity
{
    public long ClienteId { get; set; }
    public string Cpf { get; set; } = default!;
    public string? Rg { get; set; }
    public DateTime? Data_Nascimento { get; set; }
    public string? Genero { get; set; }
    public string? Estado_Civil { get; set; }
    public string? Profissao { get; set; }
}


