namespace Oficina.Estoque.Api;
public record FabricanteDto(long Id, string Nome, string? Cnpj, string? Contato);
public record CategoriaDto(long Id, string Nome, string? Descricao);
public record LocalizacaoDto(long Id, string Descricao, string? Corredor, string? Prateleira);
public record PecaFornecedorDto(long FornecedorId, decimal? Preco, int? PrazoEntrega, string? Observacao);
public record PecaAnexoDto(string Nome, string Tipo, string Url, string? Observacao);
public record PecaHistoricoDto(DateTime DataAlteracao, string Usuario, string Campo, string? ValorAntigo, string? ValorNovo);
public record PecaCreateDto(
 string Codigo,
 string Descricao,
 decimal PrecoUnitario,
 int Quantidade,
 int EstoqueMinimo,
 int EstoqueMaximo,
 string Unidade,
 string Status,
 string? Observacoes,
 long? FabricanteId,
 long? CategoriaId,
 long? LocalizacaoId,
 IReadOnlyCollection<PecaFornecedorDto>? Fornecedores,
 IReadOnlyCollection<PecaAnexoDto>? Anexos
);
public record MovimentacaoCreateDto(long PecaId, int Quantidade, string Tipo, string? Referencia, string? Usuario);


