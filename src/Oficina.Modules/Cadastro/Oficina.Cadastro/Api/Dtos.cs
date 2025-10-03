namespace Oficina.Cadastro.Api;
public record ClienteCreateDto(string Nome, string Documento, string Telefone, string Email, DateTime CreatedAt);
public record MecanicoCreateDto(string Nome, string? Especialidade);
public record FornecedorCreateDto(string RazaoSocial, string Cnpj, string Contato, long? Fornecedor_Id);
