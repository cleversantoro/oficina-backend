using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly CadastroDbContext _db;
    public ClienteRepository(CadastroDbContext db) => _db = db;

    public async Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Clientes.FindAsync(new object?[] { id }, ct);

    public async Task<List<Cliente>> SearchAsync(string? termo, int skip, int take, CancellationToken ct = default)
    {
        var query = _db.Clientes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(termo))
        {
            var normalized = termo.Trim();
            var pattern = $"%{normalized}%";
            query = query.Where(x =>
                EF.Functions.ILike(x.Nome, pattern) ||
                x.Documento.Contains(normalized));
        }

        return await query
            .OrderBy(x => x.Nome)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Cliente entity, CancellationToken ct = default)
    {
        await _db.Clientes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> DocumentoExisteAsync(string documento, Guid? ignoreId = null, CancellationToken ct = default)
    {
        var query = _db.Clientes.AsNoTracking().Where(x => x.Documento == documento);
        if (ignoreId.HasValue)
        {
            query = query.Where(x => x.Id != ignoreId.Value);
        }

        return query.AnyAsync(ct);
    }
}
