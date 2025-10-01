namespace Oficina.Estoque.Api;
public record PecaCreateDto(string Codigo, string Descricao, decimal PrecoUnitario, int Quantidade, System.Guid? FornecedorId);
public record MovimentacaoCreateDto(System.Guid PecaId, int Quantidade, string Tipo, string? Referencia);
