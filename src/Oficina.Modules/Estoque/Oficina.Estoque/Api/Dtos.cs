namespace Oficina.Estoque.Api;
public record PecaCreateDto(string Codigo, string Descricao, decimal PrecoUnitario, int Quantidade, long? FornecedorId);
public record MovimentacaoCreateDto(long PecaId, int Quantidade, string Tipo, string? Referencia);
