namespace Oficina.Financeiro.Api;
public record PagamentoCreateDto(long OrdemServicoId, string Meio, decimal Valor);
public record AtualizaStatusDto(string Status);
public record NfeCreateDto(long OrdemServicoId);
public record MetodoPagamentoDto(string Nome, string? Descricao);
public record ContaPagarDto(long? FornecedorId, string Descricao, decimal Valor, DateTime Vencimento, string Status, DateTime? DataPagamento, long? MetodoId, string? Observacao);
public record ContaReceberDto(long? ClienteId, string Descricao, decimal Valor, DateTime Vencimento, string Status, DateTime? DataRecebimento, long? MetodoId, string? Observacao);
public record LancamentoDto(string Tipo, string Descricao, decimal Valor, DateTime DataLancamento, string? Referencia, string? Observacao);
public record FinanceiroAnexoDto(long? PagamentoId, long? ContaPagarId, long? ContaReceberId, string? Nome, string? Tipo, string? Url, string? Observacao);
public record FinanceiroHistoricoDto(string Entidade, long EntidadeId, DateTime DataAlteracao, string? Usuario, string? Campo, string? ValorAntigo, string? ValorNovo);
public record PagamentoDto(long OrdemServicoId, string Meio, decimal Valor, string Status, string? TransacaoId, long? ClienteId, long? FornecedorId, long? MetodoId, string? Observacao, DateTime? DataPagamento);


