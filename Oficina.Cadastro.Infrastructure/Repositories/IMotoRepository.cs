using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Repositories;

public interface IMotoRepository
{
    Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Moto>> SearchAsync(Guid? clienteId, string? placa, int skip, int take, CancellationToken ct = default);
    Task AddAsync(Moto entity, CancellationToken ct = default);
    Task UpdateAsync(Moto entity, CancellationToken ct = default);
    Task DeleteAsync(Moto entity, CancellationToken ct = default);
    Task<bool> PlacaExisteAsync(string placa, Guid? ignoreId = null, CancellationToken ct = default);
}
