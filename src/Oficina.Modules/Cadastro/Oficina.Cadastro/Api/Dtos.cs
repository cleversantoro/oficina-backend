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
public record ClienteDocumentoDto(string Tipo, string Documento, DateTime? DataEmissao, DateTime? DataValidade, string? OrgaoExpedidor, bool Principal);
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
    IReadOnlyCollection<ClienteDocumentoDto>? Documentos,
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
    IReadOnlyCollection<ClienteDocumentoDto>? Documentos,
    string? Observacoes
);

public record ClienteResumoDto(
    long Codigo, 
    long Id, 
    string Nome, 
    ClienteStatus Status, 
    ClienteOrigemDto? Origem, 
    bool Vip, 
    ClienteTipo Tipo
);

public record ClienteDetalhesDto(
    long Id,
    long Codigo,
    string Nome,
    ClienteTipo Tipo,
    ClienteStatus Status,
    string Email,
    bool Vip,
    string? Observacoes,
    DateTime? Created_At,
    DateTime? Update_At,
    ClienteOrigemDto? Origem,
    ClientePessoaFisicaDto? PessoaFisica,
    ClientePessoaJuridicaDto? PessoaJuridica,
    IReadOnlyCollection<ClienteEnderecoDto> Enderecos,
    IReadOnlyCollection<ClienteContatoDto> Contatos,
    IReadOnlyCollection<ClienteConsentimentoDto> Consentimentos,
    IReadOnlyCollection<ClienteVeiculoDto> Veiculos,
    IReadOnlyCollection<ClienteAnexoDto> Anexos,
    IReadOnlyCollection<ClienteDocumentoDto> Documentos
);
public record ClienteFiltroDto(ClienteStatus? Status = null, ClienteTipo? Tipo = null, long? OrigemId = null, bool? Vip = null, string? Nome = null);

public record VeiculoFiltroDto(long? ClienteId = null, long? ClienteCodigo = null, string? Placa = null, long? ModeloId = null, bool? Principal = null);
public record VeiculoCreateDto(
    long ClienteId,
    string Placa,
    string? Marca,
    long? ModeloId,
    int? Ano,
    string? Cor,
    string? Chassi,
    string? Renavam,
    string? Combustivel,
    string? Observacao,
    bool Principal);
public record VeiculoUpdateDto(
    long ClienteId,
    string Placa,
    string? Marca,
    long? ModeloId,
    int? Ano,
    string? Cor,
    string? Chassi,
    string? Renavam,
    string? Combustivel,
    string? Observacao,
    bool Principal);
public record VeiculoResumoDto(
    long Id,
    long ClienteId,
    long ClienteCodigo,
    string ClienteNome,
    string Placa,
    string? Marca,
    string? ModeloNome,
    bool Principal);
public record VeiculoDetalhesDto(
    long Id,
    long ClienteId,
    long ClienteCodigo,
    string ClienteNome,
    string Placa,
    string? Marca,
    long? ModeloId,
    string? ModeloNome,
    int? Ano,
    string? Cor,
    string? Chassi,
    string? Renavam,
    string? Combustivel,
    string? Observacao,
    bool Principal);

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

public record FornecedorEnderecoDto(string Tipo, string Cep, string Logradouro, string Numero, string Bairro, string Cidade, string Estado, string Pais, string? Complemento, bool Principal);
public record FornecedorContatoDto(string Tipo, string Valor, bool Principal, string? Observacao);
public record FornecedorAnexoDto(string Nome, string Tipo, string Url, string? Observacao);
public record FornecedorBancoDto(string Banco, string Agencia, string Conta, string TipoConta, string Titular, string DocumentoTitular, string? PixChave);
public record FornecedorHistoricoDto(DateTime DataAlteracao, string Usuario, string Campo, string? ValorAntigo, string? ValorNovo);
public record FornecedorCreateDto(
 string RazaoSocial,
 string? NomeFantasia,
 string Cnpj,
 string? InscricaoEstadual,
 string? Contato,
 string? Email,
 string? Telefone,
 string? Observacoes,
 string Status,
 IReadOnlyCollection<FornecedorEnderecoDto>? Enderecos,
 IReadOnlyCollection<FornecedorContatoDto>? Contatos,
 IReadOnlyCollection<FornecedorAnexoDto>? Anexos,
 IReadOnlyCollection<FornecedorBancoDto>? Bancos
);
public record FornecedorDetalhesDto(
 long Id,
 string RazaoSocial,
 string? NomeFantasia,
 string Cnpj,
 string? InscricaoEstadual,
 string? Contato,
 string? Email,
 string? Telefone,
 string? Observacoes,
 string Status,
 IReadOnlyCollection<FornecedorEnderecoDto> Enderecos,
 IReadOnlyCollection<FornecedorContatoDto> Contatos,
 IReadOnlyCollection<FornecedorAnexoDto> Anexos,
 IReadOnlyCollection<FornecedorBancoDto> Bancos,
 IReadOnlyCollection<FornecedorHistoricoDto> Historicos
);

