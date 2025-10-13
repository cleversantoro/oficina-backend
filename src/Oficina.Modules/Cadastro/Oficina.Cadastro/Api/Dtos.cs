using Oficina.Cadastro.Domain;

namespace Oficina.Cadastro.Api;

public record ClienteOrigemDto(long Id, string Nome, string? Descricao);

public record ClientePessoaFisicaDto(string Cpf, string? Rg, DateTime? DataNascimento, string? Genero);

public record ClientePessoaJuridicaDto(string Cnpj, string RazaoSocial, string? NomeFantasia, string? InscricaoEstadual, string? Responsavel);

public record ClienteEnderecoDto(ClienteEnderecoTipo Tipo, string Cep, string Logradouro, string Numero, string Bairro, string Cidade, string Estado, string Pais, string? Complemento, bool Principal);

public record ClienteContatoDto(ClienteContatoTipo Tipo, string Valor, bool Principal, string? Observacao);

public record ClienteConsentimentoDto(ClienteConsentimentoTipo Tipo, bool Aceito, DateTime? Data, DateTime? ValidoAte, string? Observacoes);

public record ClienteVeiculoDto(string Placa, string? Marca, long? ModeloId, string? ModeloNome, int? Ano, string? Cor, string? Chassi, bool Principal);

public record ClienteAnexoDto(string Nome, string Tipo, string Url, string? Observacao);

public record ClienteCreateDto(
    string Nome,
    ClienteTipo Tipo,
    ClienteStatus Status,
    long OrigemId,
    bool Vip,
    ClientePessoaFisicaDto? PessoaFisica,
    ClientePessoaJuridicaDto? PessoaJuridica,
    IReadOnlyCollection<ClienteEnderecoDto>? Enderecos,
    IReadOnlyCollection<ClienteContatoDto>? Contatos,
    IReadOnlyCollection<ClienteConsentimentoDto>? Consentimentos,
    IReadOnlyCollection<ClienteVeiculoDto>? Veiculos,
    IReadOnlyCollection<ClienteAnexoDto>? Anexos,
    string? Observacoes
);

public record ClienteUpdateDto(
    string Nome,
    ClienteTipo Tipo,
    ClienteStatus Status,
    long OrigemId,
    bool Vip,
    ClientePessoaFisicaDto? PessoaFisica,
    ClientePessoaJuridicaDto? PessoaJuridica,
    ClienteConsentimentoDto? Consentimento,
    IReadOnlyCollection<ClienteEnderecoDto>? Enderecos,
    IReadOnlyCollection<ClienteContatoDto>? Contatos,
    IReadOnlyCollection<ClienteVeiculoDto>? Veiculos,
    IReadOnlyCollection<ClienteAnexoDto>? Anexos,
    string? Observacoes
);

public record ClienteResumoDto(long Codigo, long Id, string Nome, ClienteStatus Status, ClienteOrigemDto? Origem, bool Vip, ClienteTipo Tipo);

public record ClienteDetalhesDto(
    long Codigo,
    long Id,
    string Nome,
    ClienteTipo Tipo,
    ClienteStatus Status,
    ClienteOrigemDto? Origem,
    bool Vip,
    string? Observacoes,
    ClientePessoaFisicaDto? PessoaFisica,
    ClientePessoaJuridicaDto? PessoaJuridica,
    IReadOnlyCollection<ClienteEnderecoDto> Enderecos,
    IReadOnlyCollection<ClienteContatoDto> Contatos,
    IReadOnlyCollection<ClienteConsentimentoDto> Consentimentos,
    IReadOnlyCollection<ClienteVeiculoDto> Veiculos,
    IReadOnlyCollection<ClienteAnexoDto> Anexos
);

public record ClienteFiltroDto(ClienteStatus? Status = null, ClienteTipo? Tipo = null, long? OrigemId = null, bool? Vip = null, string? Nome = null);

public record MecanicoCreateDto(string Nome, string? Especialidade);

public record FornecedorCreateDto(string RazaoSocial, string Cnpj, string Contato, long? Fornecedor_Id);
