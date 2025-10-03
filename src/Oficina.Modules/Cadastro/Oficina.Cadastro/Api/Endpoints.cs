using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Api;
using Oficina.Cadastro.Domain;
using Oficina.Cadastro.Infrastructure;

namespace Oficina.Cadastro;

public static class Endpoints
{
    public static void MapCadastroEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/cadastro").WithTags("Cadastro");

        g.MapGet("/clientes", async ([AsParameters] ClienteFiltroDto filtro, CadastroDbContext db) =>
        {
            var query = db.Clientes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                var nome = filtro.Nome.Trim();
                query = query.Where(c => EF.Functions.Like(c.Nome, $"%{nome}%"));
            }

            if (filtro.Status is not null)
            {
                query = query.Where(c => c.Status == filtro.Status);
            }

            if (filtro.Tipo is not null)
            {
                query = query.Where(c => c.Tipo == filtro.Tipo);
            }

            if (filtro.Origem is not null)
            {
                query = query.Where(c => c.Origem == filtro.Origem);
            }

            if (filtro.Vip is not null)
            {
                query = query.Where(c => c.Vip == filtro.Vip);
            }

            var result = await query
                .OrderBy(c => c.Nome)
                .Select(c => new ClienteResumoDto(c.Codigo, c.Id, c.Nome, c.Status, c.Origem, c.Vip, c.Tipo))
                .ToListAsync();

            return Results.Ok(result);
        }).WithSummary("Lista clientes com filtros");

        g.MapGet("/clientes/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var cliente = await CarregarClientePorCodigo(db, id);
            return cliente is null ? Results.NotFound() : Results.Ok(MapToDetalhesDto(cliente));
        }).WithSummary("Obtém cliente por código");

        g.MapPost("/clientes", async (ClienteCreateDto dto, CadastroDbContext db, IValidator<ClienteCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            var cliente = MapToEntity(dto);

            await using var transaction = await db.Database.BeginTransactionAsync();
            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            var created = await CarregarClientePorId(db, cliente.Id);
            return Results.Created($"/cadastro/clientes/{cliente.Codigo}", MapToDetalhesDto(created!));
        }).WithSummary("Cria cliente");

        g.MapPut("/clientes/{id:long}", async (long id, ClienteUpdateDto dto, CadastroDbContext db, IValidator<ClienteUpdateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            var cliente = await CarregarClientePorCodigo(db, id, track: true);
            if (cliente is null)
            {
                return Results.NotFound();
            }

            AtualizarCliente(cliente, dto, db);

            await using var transaction = await db.Database.BeginTransactionAsync();
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            var atualizado = await CarregarClientePorId(db, cliente.Id);
            return Results.Ok(MapToDetalhesDto(atualizado!));
        }).WithSummary("Atualiza cliente");

        g.MapDelete("/clientes/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Codigo == id);
            if (cliente is null)
            {
                return Results.NotFound();
            }

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui cliente");

        g.MapGet("/mecanicos", async (CadastroDbContext db) =>
            Results.Ok(await db.Mecanicos.AsNoTracking().ToListAsync())).WithSummary("Lista mecânicos");

        g.MapPost("/mecanicos", async (MecanicoCreateDto dto, CadastroDbContext db, IValidator<MecanicoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
            {
                return Results.ValidationProblem(vr.ToDictionary());
            }

            var m = new Mecanico { Nome = dto.Nome, Especialidade = dto.Especialidade ?? "Geral" };
            db.Mecanicos.Add(m);
            await db.SaveChangesAsync();
            return Results.Created($"/cadastro/mecanicos/{m.Id}", m);
        }).WithSummary("Cria mecânico");

        g.MapGet("/fornecedores", async (CadastroDbContext db) =>
            Results.Ok(await db.Fornecedores.AsNoTracking().ToListAsync())).WithSummary("Lista fornecedores");

        g.MapPost("/fornecedores", async (FornecedorCreateDto dto, CadastroDbContext db, IValidator<FornecedorCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
            {
                return Results.ValidationProblem(vr.ToDictionary());
            }

            var f = new Fornecedor { Razao_Social = dto.RazaoSocial, Cnpj = dto.Cnpj, Contato = dto.Contato };
            db.Fornecedores.Add(f);
            await db.SaveChangesAsync();
            return Results.Created($"/cadastro/fornecedores/{f.Id}", f);
        }).WithSummary("Cria fornecedor");
    }

    private static Cliente MapToEntity(ClienteCreateDto dto)
    {
        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Tipo = dto.Tipo,
            Status = dto.Status,
            Origem = dto.Origem,
            Vip = dto.Vip,
            Observacoes = dto.Observacoes
        };

        if (dto.PessoaFisica is not null && dto.Tipo == ClienteTipo.PessoaFisica)
        {
            cliente.PessoaFisica = new ClientePessoaFisica
            {
                Cliente_Id = cliente.Id,
                Cpf = dto.PessoaFisica.Cpf,
                Rg = dto.PessoaFisica.Rg,
                Data_Nascimento = dto.PessoaFisica.DataNascimento,
                Genero = dto.PessoaFisica.Genero
            };
        }

        if (dto.PessoaJuridica is not null && dto.Tipo == ClienteTipo.PessoaJuridica)
        {
            cliente.PessoaJuridica = new ClientePessoaJuridica
            {
                Cliente_Id = cliente.Id,
                Cnpj = dto.PessoaJuridica.Cnpj,
                Razao_Social = dto.PessoaJuridica.RazaoSocial,
                Nome_Fantasia = dto.PessoaJuridica.NomeFantasia,
                Inscricao_Estadual = dto.PessoaJuridica.InscricaoEstadual,
                Responsavel = dto.PessoaJuridica.Responsavel
            };
        }

        cliente.Enderecos = dto.Enderecos?.Select(e => new ClienteEndereco
        {
            Cliente_Id = cliente.Id,
            Tipo = e.Tipo,
            Cep = e.Cep,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Bairro = e.Bairro,
            Cidade = e.Cidade,
            Estado = e.Estado,
            Pais = e.Pais,
            Complemento = e.Complemento,
            Principal = e.Principal
        }).ToList() ?? new List<ClienteEndereco>();

        cliente.Contatos = dto.Contatos?.Select(c => new ClienteContato
        {
            Cliente_Id = cliente.Id,
            Tipo = c.Tipo,
            Valor = c.Valor,
            Principal = c.Principal,
            Observacao = c.Observacao
        }).ToList() ?? new List<ClienteContato>();

        cliente.Consentimentos = dto.Consentimentos?.Select(c => new ClienteConsentimento
        {
            Cliente_Id = cliente.Id,
            Tipo = c.Tipo,
            Aceito = c.Aceito,
            Data = c.Data,
            Valido_Ate = c.ValidoAte,
            Observacoes = c.Observacoes
        }).ToList() ?? new List<ClienteConsentimento>();

        cliente.Veiculos = dto.Veiculos?.Select(v => new ClienteVeiculo
        {
            Cliente_Id = cliente.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Ano = v.Ano,
            Cor = v.Cor,
            Chassi = v.Chassi,
            Principal = v.Principal
        }).ToList() ?? new List<ClienteVeiculo>();

        cliente.Anexos = dto.Anexos?.Select(a => new ClienteAnexo
        {
            Cliente_Id = cliente.Id,
            Nome = a.Nome,
            Tipo = a.Tipo,
            Url = a.Url,
            Observacao = a.Observacao
        }).ToList() ?? new List<ClienteAnexo>();

        return cliente;
    }

    private static void AtualizarCliente(Cliente cliente, ClienteUpdateDto dto, CadastroDbContext db)
    {
        cliente.Nome = dto.Nome;
        cliente.Tipo = dto.Tipo;
        cliente.Status = dto.Status;
        cliente.Origem = dto.Origem;
        cliente.Vip = dto.Vip;
        cliente.Observacoes = dto.Observacoes;
        cliente.Touch();

        if (dto.Tipo == ClienteTipo.PessoaFisica)
        {
            if (cliente.PessoaFisica is null)
            {
                cliente.PessoaFisica = new ClientePessoaFisica { Cliente_Id = cliente.Id };
            }

            cliente.PessoaFisica.Cpf = dto.PessoaFisica!.Cpf;
            cliente.PessoaFisica.Rg = dto.PessoaFisica.Rg;
            cliente.PessoaFisica.Data_Nascimento = dto.PessoaFisica.DataNascimento;
            cliente.PessoaFisica.Genero = dto.PessoaFisica.Genero;

            if (cliente.PessoaJuridica is not null)
            {
                db.ClientesPessoaJuridica.Remove(cliente.PessoaJuridica);
                cliente.PessoaJuridica = null;
            }
        }
        else if (dto.Tipo == ClienteTipo.PessoaJuridica)
        {
            if (cliente.PessoaJuridica is null)
            {
                cliente.PessoaJuridica = new ClientePessoaJuridica { Cliente_Id = cliente.Id };
            }

            cliente.PessoaJuridica.Cnpj = dto.PessoaJuridica!.Cnpj;
            cliente.PessoaJuridica.Razao_Social = dto.PessoaJuridica.RazaoSocial;
            cliente.PessoaJuridica.Nome_Fantasia = dto.PessoaJuridica.NomeFantasia;
            cliente.PessoaJuridica.Inscricao_Estadual = dto.PessoaJuridica.InscricaoEstadual;
            cliente.PessoaJuridica.Responsavel = dto.PessoaJuridica.Responsavel;

            if (cliente.PessoaFisica is not null)
            {
                db.ClientesPessoaFisica.Remove(cliente.PessoaFisica);
                cliente.PessoaFisica = null;
            }
        }

        db.ClientesEnderecos.RemoveRange(cliente.Enderecos);
        cliente.Enderecos = dto.Enderecos?.Select(e => new ClienteEndereco
        {
            Cliente_Id = cliente.Id,
            Tipo = e.Tipo,
            Cep = e.Cep,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Bairro = e.Bairro,
            Cidade = e.Cidade,
            Estado = e.Estado,
            Pais = e.Pais,
            Complemento = e.Complemento,
            Principal = e.Principal
        }).ToList() ?? new List<ClienteEndereco>();

        db.ClientesContatos.RemoveRange(cliente.Contatos);
        cliente.Contatos = dto.Contatos?.Select(c => new ClienteContato
        {
            Cliente_Id = cliente.Id,
            Tipo = c.Tipo,
            Valor = c.Valor,
            Principal = c.Principal,
            Observacao = c.Observacao
        }).ToList() ?? new List<ClienteContato>();

        db.ClientesConsentimentos.RemoveRange(cliente.Consentimentos);
        cliente.Consentimentos = dto.Consentimentos?.Select(c => new ClienteConsentimento
        {
            Cliente_Id = cliente.Id,
            Tipo = c.Tipo,
            Aceito = c.Aceito,
            Data = c.Data,
            Valido_Ate = c.ValidoAte,
            Observacoes = c.Observacoes
        }).ToList() ?? new List<ClienteConsentimento>();

        db.ClientesVeiculos.RemoveRange(cliente.Veiculos);
        cliente.Veiculos = dto.Veiculos?.Select(v => new ClienteVeiculo
        {
            Cliente_Id = cliente.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Ano = v.Ano,
            Cor = v.Cor,
            Chassi = v.Chassi,
            Principal = v.Principal
        }).ToList() ?? new List<ClienteVeiculo>();

        db.ClientesAnexos.RemoveRange(cliente.Anexos);
        cliente.Anexos = dto.Anexos?.Select(a => new ClienteAnexo
        {
            Cliente_Id = cliente.Id,
            Nome = a.Nome,
            Tipo = a.Tipo,
            Url = a.Url,
            Observacao = a.Observacao
        }).ToList() ?? new List<ClienteAnexo>();
    }

    private static async Task<Cliente?> CarregarClientePorCodigo(CadastroDbContext db, long codigo, bool track = false)
    {
        var query = db.Clientes
            .Include(c => c.PessoaFisica)
            .Include(c => c.PessoaJuridica)
            .Include(c => c.Enderecos)
            .Include(c => c.Contatos)
            .Include(c => c.Consentimentos)
            .Include(c => c.Veiculos)
            .Include(c => c.Anexos)
            .Where(c => c.Codigo == codigo);

        if (!track)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    private static async Task<Cliente?> CarregarClientePorId(CadastroDbContext db, Guid id)
    {
        return await db.Clientes
            .Include(c => c.PessoaFisica)
            .Include(c => c.PessoaJuridica)
            .Include(c => c.Enderecos)
            .Include(c => c.Contatos)
            .Include(c => c.Consentimentos)
            .Include(c => c.Veiculos)
            .Include(c => c.Anexos)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    private static ClienteDetalhesDto MapToDetalhesDto(Cliente cliente)
    {
        return new ClienteDetalhesDto(
            cliente.Codigo,
            cliente.Id,
            cliente.Nome,
            cliente.Tipo,
            cliente.Status,
            cliente.Origem,
            cliente.Vip,
            cliente.Observacoes,
            cliente.PessoaFisica is null
                ? null
                : new ClientePessoaFisicaDto(
                    cliente.PessoaFisica.Cpf,
                    cliente.PessoaFisica.Rg,
                    cliente.PessoaFisica.Data_Nascimento,
                    cliente.PessoaFisica.Genero),
            cliente.PessoaJuridica is null
                ? null
                : new ClientePessoaJuridicaDto(
                    cliente.PessoaJuridica.Cnpj,
                    cliente.PessoaJuridica.Razao_Social,
                    cliente.PessoaJuridica.Nome_Fantasia,
                    cliente.PessoaJuridica.Inscricao_Estadual,
                    cliente.PessoaJuridica.Responsavel),
            cliente.Enderecos.Select(e => new ClienteEnderecoDto(
                e.Tipo,
                e.Cep,
                e.Logradouro,
                e.Numero,
                e.Bairro,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Complemento,
                e.Principal)).ToList(),
            cliente.Contatos.Select(c => new ClienteContatoDto(c.Tipo, c.Valor, c.Principal, c.Observacao)).ToList(),
            cliente.Consentimentos.Select(c => new ClienteConsentimentoDto(c.Tipo, c.Aceito, c.Data, c.Valido_Ate, c.Observacoes)).ToList(),
            cliente.Veiculos.Select(v => new ClienteVeiculoDto(v.Placa, v.Marca, v.Modelo, v.Ano, v.Cor, v.Chassi, v.Principal)).ToList(),
            cliente.Anexos.Select(a => new ClienteAnexoDto(a.Nome, a.Tipo, a.Url, a.Observacao)).ToList()
        );
    }
}
