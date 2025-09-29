using AutoMapper;
using Oficina.Cadastro.Application.Dtos;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Repositories;
using Oficina.SharedKernel.ValueObjects;

namespace Oficina.Cadastro.Application.Services;

public class MotoAppService : IMotoAppService
{
    private readonly IMotoRepository _repo;
    private readonly IMapper _mapper;

    public MotoAppService(IMotoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<MotoDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : _mapper.Map<MotoDto>(entity);
    }

    public async Task<List<MotoDto>> SearchAsync(Guid? clienteId, string? placa, int page, int pageSize, CancellationToken ct = default)
    {
        var take = Math.Max(1, pageSize);
        var skip = Math.Max(0, page - 1) * take;
        var list = await _repo.SearchAsync(clienteId, placa, skip, take, ct);
        return list.Select(_mapper.Map<MotoDto>).ToList();
    }

    public async Task<Guid> CreateAsync(CreateMotoRequest req, CancellationToken ct = default)
    {
        // placa única
        if (await _repo.PlacaExisteAsync(req.Placa, null, ct))
            throw new InvalidOperationException("Placa já cadastrada");

        var entity = new Moto(
            clienteId: req.ClienteId,
            marca: req.Marca,
            modelo: req.Modelo,
            ano: req.Ano,
            placa: new Placa(req.Placa),
            chassi: req.Chassi,
            kmAtual: req.KmAtual
        );

        await _repo.AddAsync(entity, ct);
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateMotoRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Moto não encontrada");

        // placa única (ignorando a própria moto)
        if (await _repo.PlacaExisteAsync(req.Placa, id, ct))
            throw new InvalidOperationException("Placa já cadastrada para outra moto");

        // Atualização de dados básicos
        entity.AtualizarDados(
            marca: req.Marca,
            modelo: req.Modelo,
            ano: req.Ano,
            placa: new Placa(req.Placa),
            chassi: req.Chassi,
            kmAtual: req.KmAtual
        );

        await _repo.UpdateAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Moto não encontrada");
        await _repo.DeleteAsync(entity, ct);
    }
}
