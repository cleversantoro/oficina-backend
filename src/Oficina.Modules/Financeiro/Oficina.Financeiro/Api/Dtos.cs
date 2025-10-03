namespace Oficina.Financeiro.Api;
public record PagamentoCreateDto(long OrdemServicoId, string Meio, decimal Valor);
public record AtualizaStatusDto(string Status);
public record NfeCreateDto(long OrdemServicoId);
