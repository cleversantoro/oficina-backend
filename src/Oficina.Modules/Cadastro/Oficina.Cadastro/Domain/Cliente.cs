using System;
using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class Cliente : Entity
{
    public long Codigo { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public string Nome { get; set; } = default!;
    public string NomeExibicao { get; set; } = default!;
    public string Documento { get; set; } = default!;
    public ClienteTipo Tipo { get; set; } = ClienteTipo.PessoaFisica;
    public ClienteStatus Status { get; set; } = ClienteStatus.Ativo;
    public bool Vip { get; set; }
    public string? Observacoes { get; set; }
    public int OrigemCadastroId { get; set; }
    public string Telefone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public long? Origem_Id { get; set; }
    public ClienteOrigem? Origem { get; set; }
    public ClientePessoaFisica? PessoaFisica { get; set; }
    public ClientePessoaJuridica? PessoaJuridica { get; set; }
    public ClienteFinanceiro? Financeiro { get; set; }
    public ClienteConsentimento? Consentimento { get; set; }
    public ICollection<ClienteEndereco> Enderecos { get; set; } = new List<ClienteEndereco>();
    public ICollection<ClienteContato> Contatos { get; set; } = new List<ClienteContato>();
    public ICollection<ClienteIndicacao> Indicacoes { get; set; } = new List<ClienteIndicacao>();
    public ICollection<ClienteVeiculo> Veiculos { get; set; } = new List<ClienteVeiculo>();
    public ICollection<ClienteAnexo> Anexos { get; set; } = new List<ClienteAnexo>();

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

