using Oficina.SharedKernel.Domain;
namespace Oficina.Cadastro.Domain;
public class Cliente : Entity
{
    public string Nome { get; set; } = default!;
    public string Documento { get; set; } = default!;
    public string Telefone { get; set; } = default!;
    public string Email { get; set; } = default!;
}
