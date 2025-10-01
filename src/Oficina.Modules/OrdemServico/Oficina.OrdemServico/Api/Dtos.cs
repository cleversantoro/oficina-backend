namespace Oficina.OrdemServico.Api;
public record OrdemCreateDto(System.Guid ClienteId, System.Guid MecanicoId, string DescricaoProblema);
public record ItemCreateDto(System.Guid? PecaId, string Descricao, int Quantidade, decimal ValorUnitario);
