namespace Oficina.OrdemServico.Api;
public record OrdemCreateDto(long ClienteId, long MecanicoId, string DescricaoProblema);
public record ItemCreateDto(long? PecaId, string Descricao, int Quantidade, decimal ValorUnitario);

