using Oficina.SharedKernel.Domain;
namespace Oficina.Cadastro.Domain;
public class Mecanico : Entity
{
    public string Nome { get; set; } = default!;
    public string Especialidade { get; set; } = "Geral";
    public bool Ativo { get; set; } = true;
}
