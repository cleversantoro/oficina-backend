namespace Oficina.Financeiro.Api;
public record PagamentoCreateDto(System.Guid OrdemServicoId, string Meio, decimal Valor);
public record AtualizaStatusDto(string Status);
public record NfeCreateDto(System.Guid OrdemServicoId);
