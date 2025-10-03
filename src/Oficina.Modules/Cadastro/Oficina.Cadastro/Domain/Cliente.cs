using System;
using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class Cliente : Entity
{
    public Guid TenantId { get; set; }
    public string Nome { get; set; } = default!;
    public string NomeExibicao { get; set; } = default!;
    public string Documento { get; set; } = default!;
    public ClientePessoaTipo PessoaTipo { get; set; } = ClientePessoaTipo.PessoaFisica;
    public ClienteStatus Status { get; set; } = ClienteStatus.Ativo;
    public bool ClienteVip { get; set; }
    public int OrigemCadastroId { get; set; }
    public string Telefone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Guid? Origem_Id { get; set; }
    public ClienteOrigem? Origem { get; set; }
    public PessoaPf? PessoaPf { get; set; }
    public PessoaPj? PessoaPj { get; set; }
    public ClienteLgpdConsentimento? LgpdConsentimento { get; set; }
    public ClienteFinanceiro? Financeiro { get; set; }
    public ICollection<ClienteEndereco> Enderecos { get; set; } = new List<ClienteEndereco>();
    public ICollection<ClienteContato> Contatos { get; set; } = new List<ClienteContato>();
    public ICollection<ClienteIndicacao> Indicacoes { get; set; } = new List<ClienteIndicacao>();
    public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    public ICollection<ClienteAnexo> Anexos { get; set; } = new List<ClienteAnexo>();
}
