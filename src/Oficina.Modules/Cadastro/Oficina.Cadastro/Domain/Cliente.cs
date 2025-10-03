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
    public DateTime? DeletedAt { get; set; }
}

public enum ClienteStatus : short
{
    Inativo = 0,
    Ativo = 1,
    Suspenso = 2,
    Bloqueado = 3
}

public enum ClientePessoaTipo : short
{
    PessoaFisica = 1,
    PessoaJuridica = 2
}
