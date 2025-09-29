using Oficina.Cadastro.Application.Dtos;

namespace Oficina.Cadastro.Application.Services;

public interface IClienteAppService
{
    Task<ClienteDto?> GetAsync(Guid id, CancellationToken ct = default);
    Task<List<ClienteDto>> SearchAsync(string? termo, int page, int pageSize, CancellationToken ct = default);
    Task<Guid> CreateAsync(CreateClienteRequest req, CancellationToken ct = default);
    Task UpdateAsync(Guid id, UpdateClienteRequest req, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
