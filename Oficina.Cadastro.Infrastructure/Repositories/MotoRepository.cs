using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Repositories;

public class MotoRepository : IMotoRepository
{
    private readonly CadastroDbContext _db;
    public MotoRepository(CadastroDbContext db) => _db = db;

    public async Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Motos.Include(x => x.Cliente).FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<List<Moto>> SearchAsync(Guid? clienteId, string? placa, int skip, int take, CancellationToken ct = default)
    {
        var query = _db.Motos.AsNoTracking();

        if (clienteId.HasValue && clienteId.Value != Guid.Empty)
        {
            query = query.Where(x => x.ClienteId == clienteId.Value);
        }

        if (!string.IsNullOrWhiteSpace(placa))
        {
            var norm = placa.ToUpper().Replace("-", string.Empty).Trim();
            var pattern = $"%{norm}%";
            query = query.Where(x => EF.Functions.ILike(x.Placa, pattern));
        }

        return await query
            .OrderBy(x => x.Marca)
            .ThenBy(x => x.Modelo)
            .ThenBy(x => x.Placa)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Moto entity, CancellationToken ct = default)
    {
        await _db.Motos.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Moto entity, CancellationToken ct = default)
    {
        _db.Motos.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Moto entity, CancellationToken ct = default)
    {
        _db.Motos.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> PlacaExisteAsync(string placa, Guid? ignoreId = null, CancellationToken ct = default)
    {
        var norm = placa.ToUpper().Replace("-", string.Empty).Trim();
        var query = _db.Motos.AsNoTracking().Where(x => x.Placa == norm);
        if (ignoreId.HasValue)
        {
            query = query.Where(x => x.Id != ignoreId.Value);
        }

        return query.AnyAsync(ct);
    }
}
