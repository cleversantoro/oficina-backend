namespace Oficina.Cadastro.Domain;

public enum ClienteTipo
{
    PessoaFisica = 1,
    PessoaJuridica = 2
}

public enum ClienteStatus
{
    Ativo = 1,
    Inativo = 2,
    Suspenso = 3
}

//public enum ClienteOrigem
//{
//    Presencial = 1,
//    Online = 2,
//    Indicacao = 3,
//    Outro = 4
//}

public enum ClienteEnderecoTipo
{
    Residencial = 1,
    Comercial = 2,
    Correspondencia = 3,
    Outro = 4
}

public enum ClienteContatoTipo
{
    Telefone = 1,
    Celular = 2,
    Email = 3,
    Whatsapp = 4,
    Outro = 5
}

public enum ClienteConsentimentoTipo
{
    Marketing = 1,
    CompartilhamentoDados = 2,
    Comunicacoes = 3,
    Outros = 99
}
