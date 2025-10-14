using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClientePessoaJuridica : Entity
{
    public long ClienteId { get; set; }
    public string Cnpj { get; set; } = default!;
    public string Razao_Social { get; set; } = default!;
    public string? Nome_Fantasia { get; set; }
    public string? Inscricao_Estadual { get; set; }
    public string? Inscricao_Municipal { get; set; }
    public string? Responsavel { get; set; }

}

