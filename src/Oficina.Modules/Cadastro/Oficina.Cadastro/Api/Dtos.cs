using System;
using System.Collections.Generic;
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

public record MecanicoEspecialidadeRelDto(long EspecialidadeId, string Nivel, bool Principal, string? Anotacoes);
public record MecanicoContatoDto(string Tipo, string Valor, bool Principal, string? Observacao);
public record MecanicoEnderecoDto(string Tipo, string Cep, string Logradouro, string Numero, string Bairro, string Cidade, string Estado, string? Pais, string? Complemento, bool Principal);
public record MecanicoDocumentoDto(string Tipo, string Numero, DateTime? DataEmissao, DateTime? DataValidade, string? OrgaoExpedidor, string? ArquivoUrl);
public record MecanicoCertificacaoDto(long? EspecialidadeId, string Titulo, string? Instituicao, DateTime? DataConclusao, DateTime? DataValidade, string? CodigoCertificacao);
public record MecanicoDisponibilidadeDto(byte DiaSemana, TimeSpan HoraInicio, TimeSpan HoraFim, int CapacidadeAtendimentos);
public record MecanicoExperienciaDto(string Empresa, string Cargo, DateTime? DataInicio, DateTime? DataFim, string? ResumoAtividades);

public record MecanicoCreateDto(
    string Codigo,
    string Nome,
    string? Sobrenome,
    string? NomeSocial,
    string DocumentoPrincipal,
    int TipoDocumento,
    DateTime DataAdmissao,
    DateTime? DataNascimento,
    DateTime? DataDemissao,
    string Status,
    long? EspecialidadePrincipalId,
    string Nivel,
    decimal ValorHora,
    int CargaHorariaSemanal,
    string? Observacoes,
    IReadOnlyCollection<MecanicoEspecialidadeRelDto>? Especialidades,
    IReadOnlyCollection<MecanicoContatoDto>? Contatos,
    IReadOnlyCollection<MecanicoEnderecoDto>? Enderecos,
    IReadOnlyCollection<MecanicoDocumentoDto>? Documentos,
    IReadOnlyCollection<MecanicoCertificacaoDto>? Certificacoes,
    IReadOnlyCollection<MecanicoDisponibilidadeDto>? Disponibilidades,
    IReadOnlyCollection<MecanicoExperienciaDto>? Experiencias
);

public record MecanicoResumoDto(
    long Id,
    string Codigo,
    string NomeCompleto,
    string Status,
    string Nivel,
    string? EspecialidadePrincipal,
    decimal ValorHora
);

public record MecanicoEspecialidadeResumoDto(long EspecialidadeId, string? EspecialidadeCodigo, string? EspecialidadeNome, string Nivel, bool Principal, string? Anotacoes);
public record MecanicoContatoResumoDto(long Id, string Tipo, string Valor, bool Principal, string? Observacao);
public record MecanicoEnderecoResumoDto(long Id, string Tipo, string Cep, string Logradouro, string Numero, string Bairro, string Cidade, string Estado, string? Pais, string? Complemento, bool Principal);
public record MecanicoDocumentoResumoDto(long Id, string Tipo, string Numero, DateTime? DataEmissao, DateTime? DataValidade, string? OrgaoExpedidor, string? ArquivoUrl);
public record MecanicoCertificacaoResumoDto(long Id, long? EspecialidadeId, string Titulo, string? Instituicao, DateTime? DataConclusao, DateTime? DataValidade, string? CodigoCertificacao, string? EspecialidadeNome);
public record MecanicoDisponibilidadeResumoDto(long Id, byte DiaSemana, TimeSpan HoraInicio, TimeSpan HoraFim, int CapacidadeAtendimentos);
public record MecanicoExperienciaResumoDto(long Id, string Empresa, string Cargo, DateTime? DataInicio, DateTime? DataFim, string? ResumoAtividades);

public record MecanicoDetalhesDto(
    long Id,
    string Codigo,
    string Nome,
    string? Sobrenome,
    string? NomeSocial,
    string DocumentoPrincipal,
    int TipoDocumento,
    DateTime DataAdmissao,
    DateTime? DataNascimento,
    DateTime? DataDemissao,
    string Status,
    long? EspecialidadePrincipalId,
    string? EspecialidadePrincipalNome,
    string Nivel,
    decimal ValorHora,
    int CargaHorariaSemanal,
    string? Observacoes,
    IReadOnlyCollection<MecanicoEspecialidadeResumoDto> Especialidades,
    IReadOnlyCollection<MecanicoContatoResumoDto> Contatos,
    IReadOnlyCollection<MecanicoEnderecoResumoDto> Enderecos,
    IReadOnlyCollection<MecanicoDocumentoResumoDto> Documentos,
    IReadOnlyCollection<MecanicoCertificacaoResumoDto> Certificacoes,
    IReadOnlyCollection<MecanicoDisponibilidadeResumoDto> Disponibilidades,
    IReadOnlyCollection<MecanicoExperienciaResumoDto> Experiencias
);

public record FornecedorCreateDto(string RazaoSocial, string Cnpj, string Contato, long? Fornecedor_Id);


