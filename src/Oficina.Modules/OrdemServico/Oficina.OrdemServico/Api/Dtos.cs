namespace Oficina.OrdemServico.Api;
public record OrdemCreateDto(long Cliente_Id, long MecanicoId, string DescricaoProblema);
public record ItemCreateDto(long? PecaId, string Descricao, int Quantidade, decimal ValorUnitario);

