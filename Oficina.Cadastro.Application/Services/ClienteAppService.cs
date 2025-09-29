using AutoMapper;
using Oficina.Cadastro.Application.Dtos;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Repositories;
using Oficina.SharedKernel.ValueObjects;

namespace Oficina.Cadastro.Application.Services;


public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository _repo;
    private readonly IMapper _mapper;
    public ClienteAppService(IClienteRepository repo, IMapper mapper)
    {
        _repo = repo; _mapper = mapper;
    }

    public async Task<ClienteDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : _mapper.Map<ClienteDto>(entity);
    }

    public async Task<List<ClienteDto>> SearchAsync(string? termo, int page, int pageSize, CancellationToken ct = default)
    {
        var skip = Math.Max(0, page - 1) * Math.Max(1, pageSize);
        var list = await _repo.SearchAsync(termo, skip, Math.Max(1, pageSize), ct);
        return list.Select(_mapper.Map<ClienteDto>).ToList();
    }

    public async Task<Guid> CreateAsync(CreateClienteRequest req, CancellationToken ct = default)
    {
        if (await _repo.DocumentoExisteAsync(req.Documento, null, ct))
            throw new InvalidOperationException("Documento já cadastrado");

        var entity = new Cliente(req.Nome, req.Documento, new Email(req.Email), new Telefone(req.Telefone), req.Endereco);
        await _repo.AddAsync(entity, ct);
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateClienteRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Cliente não encontrado");

        if (await _repo.DocumentoExisteAsync(req.Documento, id, ct))
            throw new InvalidOperationException("Documento já cadastrado para outro cliente");

        entity.SetNome(req.Nome);
        entity.AlterarContato(new Email(req.Email), new Telefone(req.Telefone));
        entity.AlterarEndereco(req.Endereco);
        await _repo.UpdateAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Cliente não encontrado");
        await _repo.DeleteAsync(entity, ct);
    }
}
