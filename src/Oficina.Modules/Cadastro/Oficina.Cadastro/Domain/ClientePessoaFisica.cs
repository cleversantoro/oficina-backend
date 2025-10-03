using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClientePessoaFisica : Entity
{
    public Guid Cliente_Id { get; set; }
    public string Cpf { get; set; } = default!;
    public string? Rg { get; set; }
    public DateTime? Data_Nascimento { get; set; }
    public string? Genero { get; set; }
}
