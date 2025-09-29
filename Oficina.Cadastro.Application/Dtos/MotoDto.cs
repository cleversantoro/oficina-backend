namespace Oficina.Cadastro.Application.Dtos;

public record MotoDto(Guid Id, Guid ClienteId, string Marca, string Modelo, int Ano, string Placa, string? Chassi, int? KmAtual);
public record CreateMotoRequest(Guid ClienteId, string Marca, string Modelo, int Ano, string Placa, string? Chassi, int? KmAtual);
public record UpdateMotoRequest(Guid ClienteId, string Marca, string Modelo, int Ano, string Placa, string? Chassi, int? KmAtual);
