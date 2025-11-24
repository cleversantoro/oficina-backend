namespace Oficina.OrdemServico.Api;

public record OrdemServicoAnexoDto(string? Nome, string? Tipo, string? Url, string? Observacao);
public record OrdemServicoHistoricoDto(DateTime DataAlteracao, string? Usuario, string? Campo, string? ValorAntigo, string? ValorNovo);
public record OrdemServicoChecklistDto(string Item, bool Realizado, string? Observacao);
public record OrdemServicoAvaliacaoDto(int Nota, string? Comentario, string? Usuario);
public record OrdemServicoPagamentoDto(decimal Valor, string Status, DateTime? DataPagamento, string? Metodo, string? Observacao);
public record OrdemServicoObservacaoDto(string? Usuario, string Texto);

public record ClienteDto(long Id, string Codigo, string Nome, string Telefone, string Email);
public record MecanicoDto(long Id, string Codigo, string Nome, string Sobrenome);

public record OrdemCreateDto(
 long ClienteId,
 long MecanicoId,
 string DescricaoProblema,
 MecanicoDto? MecanicoDto,
 ClienteDto? Cliente,
 IReadOnlyCollection<ItemServicoDto>? Itens,
 IReadOnlyCollection<OrdemServicoAnexoDto>? Anexos,
 IReadOnlyCollection<OrdemServicoChecklistDto>? Checklists
);
public record ItemServicoDto(long? Peca_Id, string Descricao, int Quantidade, decimal ValorUnitario);


