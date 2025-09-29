using Oficina.Cadastro.Application.Dtos;

namespace Oficina.Cadastro.Application.Services;

public interface IMotoAppService
{
    Task<MotoDto?> GetAsync(Guid id, CancellationToken ct = default);
    Task<List<MotoDto>> SearchAsync(Guid? clienteId, string? placa, int page, int pageSize, CancellationToken ct = default);
    Task<Guid> CreateAsync(CreateMotoRequest req, CancellationToken ct = default);
    Task UpdateAsync(Guid id, UpdateMotoRequest req, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
