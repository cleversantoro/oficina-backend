using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Cliente>> SearchAsync(string? termo, int skip, int take, CancellationToken ct = default);
    Task AddAsync(Cliente entity, CancellationToken ct = default);
    Task UpdateAsync(Cliente entity, CancellationToken ct = default);
    Task DeleteAsync(Cliente entity, CancellationToken ct = default);
    Task<bool> DocumentoExisteAsync(string documento, Guid? ignoreId = null, CancellationToken ct = default);
}
