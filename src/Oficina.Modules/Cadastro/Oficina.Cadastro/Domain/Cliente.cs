using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class Cliente : Entity
{
    public long Codigo { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public string Nome { get; set; } = default!;
    public ClienteTipo Tipo { get; set; }
    public ClienteStatus Status { get; set; } = ClienteStatus.Ativo;
    public ClienteOrigem Origem { get; set; } = ClienteOrigem.Outro;
    public bool Vip { get; set; }
    public string? Observacoes { get; set; }

    public ClientePessoaFisica? PessoaFisica { get; set; }
    public ClientePessoaJuridica? PessoaJuridica { get; set; }
    public List<ClienteEndereco> Enderecos { get; set; } = new();
    public List<ClienteContato> Contatos { get; set; } = new();
    public List<ClienteConsentimento> Consentimentos { get; set; } = new();
    public List<ClienteVeiculo> Veiculos { get; set; } = new();
    public List<ClienteAnexo> Anexos { get; set; } = new();

    public void DefinirCodigo(long codigo)
    {
        if (codigo > 0)
        {
            Codigo = codigo;
        }
        else if (Codigo == 0)
        {
            Codigo = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
