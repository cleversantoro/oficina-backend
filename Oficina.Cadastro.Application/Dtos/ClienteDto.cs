namespace Oficina.Cadastro.Application.Dtos;

public record ClienteDto(Guid Id, string Nome, string Documento, string Email, string Telefone, string? Endereco);
public record CreateClienteRequest(string Nome, string Documento, string Email, string Telefone, string? Endereco);
public record UpdateClienteRequest(string Nome, string Documento, string Email, string Telefone, string? Endereco);
