using System;
using System.Collections.Generic;
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
        var g = app.MapGroup("/cadastro").WithTags("Cadastro - Clientes");

        //Clientes
        g.MapGet("/clientes", async ([AsParameters] ClienteFiltroDto filtro, CadastroDbContext db) =>
        {
            var query = db.Clientes
                .AsNoTracking()
                .Include(c => c.Origem)
                .AsQueryable();

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

            if (filtro.OrigemId is not null)
            {
                query = query.Where(c => c.Origem_Id == filtro.OrigemId);
            }

            if (filtro.Vip is not null)
            {
                query = query.Where(c => c.Vip == filtro.Vip);
            }

            var result = await query
                .OrderBy(c => c.Nome)
                .Select(c => new ClienteResumoDto(c.Codigo, c.Id, c.Nome, c.Status, ToOrigemDto(c.Origem), c.Vip, c.Tipo))
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

            if (!await db.ClienteOrigens.AnyAsync(o => o.Id == dto.OrigemId))
            {
                return Results.BadRequest("Origem informada nÃ£o existe.");
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

            if (!await db.ClienteOrigens.AnyAsync(o => o.Id == dto.OrigemId))
            {
                return Results.BadRequest("Origem informada nÃ£o existe.");
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

        //mecanicos
        var m = app.MapGroup("/cadastro").WithTags("Cadastro - Mecânicos");

        m.MapGet("/mecanicos", async (CadastroDbContext db) =>
        {
            var mecanicos = await db.Mecanicos
                .AsNoTracking()
                .Include(m => m.EspecialidadePrincipal)
                .Include(m => m.Especialidades)
                .ThenInclude(e => e.Especialidade)
                .ToListAsync();

            var result = mecanicos
                .OrderBy(m => m.Nome)
                .ThenBy(m => m.Sobrenome)
                .Select(MapToMecanicoResumoDto)
                .ToList();

            return Results.Ok(result);
        }).WithSummary("Lista mecânicos");

        m.MapGet("/mecanicos/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var mecanico = await CarregarMecanicoCompleto(db, id);
            return mecanico is null ? Results.NotFound() : Results.Ok(MapToMecanicoDetalhesDto(mecanico));
        }).WithSummary("Obtém mecânico por ID");

        m.MapPost("/mecanicos", async (MecanicoCreateDto dto, CadastroDbContext db, IValidator<MecanicoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
            {
                return Results.ValidationProblem(vr.ToDictionary());
            }

            if (await db.Mecanicos.AnyAsync(m => m.Codigo == dto.Codigo))
            {
                return Results.BadRequest("Código informado já está em uso.");
            }

            if (await db.Mecanicos.AnyAsync(m => m.Documento_Principal == dto.DocumentoPrincipal))
            {
                return Results.BadRequest("Documento informado ja esta em uso.");
            }

            if (dto.EspecialidadePrincipalId.HasValue &&
                !await db.MecanicoEspecialidades.AnyAsync(e => e.Id == dto.EspecialidadePrincipalId.Value))
            {
                return Results.BadRequest("Especialidade principal informada nao existe.");
            }

            if (dto.Especialidades is { Count: > 0 })
            {
                var especialidadeIds = dto.Especialidades.Select(e => e.EspecialidadeId).Distinct().ToList();
                if (especialidadeIds.Count > 0)
                {
                    var existentes = await db.MecanicoEspecialidades
                        .Where(e => especialidadeIds.Contains(e.Id))
                        .Select(e => e.Id)
                        .ToListAsync();
                    var faltantes = especialidadeIds.Except(existentes).ToArray();
                    if (faltantes.Length > 0)
                    {
                        return Results.BadRequest($"Especialidades nao encontradas: {string.Join(", ", faltantes)}");
                    }
                }
            }

            if (dto.Certificacoes is { Count: > 0 })
            {
                var especialidadesCert = dto.Certificacoes
                    .Where(c => c.EspecialidadeId.HasValue)
                    .Select(c => c.EspecialidadeId!.Value)
                    .Distinct()
                    .ToList();

                if (especialidadesCert.Count > 0)
                {
                    var existentes = await db.MecanicoEspecialidades
                        .Where(e => especialidadesCert.Contains(e.Id))
                        .Select(e => e.Id)
                        .ToListAsync();
                    var faltantes = especialidadesCert.Except(existentes).ToArray();
                    if (faltantes.Length > 0)
                    {
                        return Results.BadRequest($"Especialidades informadas nas certificacoes nao foram encontradas: {string.Join(", ", faltantes)}");
                    }
                }
            }

            var mecanico = MapToMecanicoEntity(dto);
            db.Mecanicos.Add(mecanico);
            await db.SaveChangesAsync();

            var created = await CarregarMecanicoCompleto(db, mecanico.Id);
            if (created is null)
            {
                return Results.Created($"/cadastro/mecanicos/{mecanico.Id}", null);
            }

            return Results.Created($"/cadastro/mecanicos/{mecanico.Id}", MapToMecanicoDetalhesDto(created));
        }).WithSummary("Cria mecânico");

        m.MapPut("/mecanicos/{id:long}", async (long id, MecanicoCreateDto dto, CadastroDbContext db, IValidator<MecanicoCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
            {
                return Results.ValidationProblem(vr.ToDictionary());
            }

            var mecanico = await db.Mecanicos
                .Include(m => m.Especialidades)
                .Include(m => m.Contatos)
                .Include(m => m.Enderecos)
                .Include(m => m.Documentos)
                .Include(m => m.Certificacoes)
                .Include(m => m.Disponibilidades)
                .Include(m => m.Experiencias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mecanico is null)
            {
                return Results.NotFound();
            }

            // Remove coleções existentes
            db.MecanicoEspecialidadesRel.RemoveRange(mecanico.Especialidades);
            db.MecanicoContatos.RemoveRange(mecanico.Contatos);
            db.MecanicoEnderecos.RemoveRange(mecanico.Enderecos);
            db.MecanicoDocumentos.RemoveRange(mecanico.Documentos);
            db.MecanicoCertificacoes.RemoveRange(mecanico.Certificacoes);
            db.MecanicoDisponibilidades.RemoveRange(mecanico.Disponibilidades);
            db.MecanicoExperiencias.RemoveRange(mecanico.Experiencias);

            // Atualiza propriedades simples
            mecanico.Codigo = dto.Codigo.Trim();
            mecanico.Nome = dto.Nome.Trim();
            mecanico.Sobrenome = Normalize(dto.Sobrenome);
            mecanico.Nome_Social = Normalize(dto.NomeSocial);
            mecanico.Documento_Principal = dto.DocumentoPrincipal.Trim();
            mecanico.Tipo_Documento = dto.TipoDocumento;
            mecanico.Data_Admissao = dto.DataAdmissao;
            mecanico.Data_Nascimento = dto.DataNascimento;
            mecanico.Data_Demissao = dto.DataDemissao;
            mecanico.Status = string.IsNullOrWhiteSpace(dto.Status) ? "Ativo" : dto.Status.Trim();
            mecanico.Especialidade_Principal_Id = dto.EspecialidadePrincipalId;
            mecanico.Nivel = string.IsNullOrWhiteSpace(dto.Nivel) ? "Pleno" : dto.Nivel.Trim();
            mecanico.Valor_Hora = dto.ValorHora;
            mecanico.Carga_Horaria_Semanal = dto.CargaHorariaSemanal;
            mecanico.Observacoes = Normalize(dto.Observacoes);

            // Atualiza coleções
            mecanico.Especialidades = dto.Especialidades?.Select(e => new MecanicoEspecialidadeRel
            {
                Especialidade_Id = e.EspecialidadeId,
                Nivel = e.Nivel.Trim(),
                Principal = e.Principal,
                Anotacoes = Normalize(e.Anotacoes)
            }).ToList() ?? new List<MecanicoEspecialidadeRel>();

            mecanico.Contatos = dto.Contatos?.Select(c => new MecanicoContato
            {
                Tipo = c.Tipo.Trim(),
                Valor = c.Valor.Trim(),
                Principal = c.Principal,
                Observacao = Normalize(c.Observacao)
            }).ToList() ?? new List<MecanicoContato>();

            mecanico.Enderecos = dto.Enderecos?.Select(e => new MecanicoEndereco
            {
                Tipo = e.Tipo.Trim(),
                Cep = e.Cep.Trim(),
                Logradouro = e.Logradouro.Trim(),
                Numero = e.Numero.Trim(),
                Bairro = e.Bairro.Trim(),
                Cidade = e.Cidade.Trim(),
                Estado = e.Estado.Trim(),
                Pais = Normalize(e.Pais),
                Complemento = Normalize(e.Complemento),
                Principal = e.Principal
            }).ToList() ?? new List<MecanicoEndereco>();

            mecanico.Documentos = dto.Documentos?.Select(d => new MecanicoDocumento
            {
                Tipo = d.Tipo.Trim(),
                Numero = d.Numero.Trim(),
                Data_Emissao = d.DataEmissao,
                Data_Validade = d.DataValidade,
                Orgao_Expedidor = Normalize(d.OrgaoExpedidor),
                Arquivo_Url = Normalize(d.ArquivoUrl)
            }).ToList() ?? new List<MecanicoDocumento>();

            mecanico.Certificacoes = dto.Certificacoes?.Select(c => new MecanicoCertificacao
            {
                Especialidade_Id = c.EspecialidadeId,
                Titulo = c.Titulo.Trim(),
                Instituicao = Normalize(c.Instituicao),
                Data_Conclusao = c.DataConclusao,
                Data_Validade = c.DataValidade,
                Codigo_Certificacao = Normalize(c.CodigoCertificacao)
            }).ToList() ?? new List<MecanicoCertificacao>();

            mecanico.Disponibilidades = dto.Disponibilidades?.Select(d => new MecanicoDisponibilidade
            {
                Dia_Semana = d.DiaSemana,
                Hora_Inicio = d.HoraInicio,
                Hora_Fim = d.HoraFim,
                Capacidade_Atendimentos = d.CapacidadeAtendimentos
            }).ToList() ?? new List<MecanicoDisponibilidade>();

            mecanico.Experiencias = dto.Experiencias?.Select(e => new MecanicoExperiencia
            {
                Empresa = e.Empresa.Trim(),
                Cargo = e.Cargo.Trim(),
                Data_Inicio = e.DataInicio,
                Data_Fim = e.DataFim,
                Resumo_Atividades = Normalize(e.ResumoAtividades)
            }).ToList() ?? new List<MecanicoExperiencia>();

            await db.SaveChangesAsync();
            var atualizado = await CarregarMecanicoCompleto(db, mecanico.Id);
            return Results.Ok(MapToMecanicoDetalhesDto(atualizado!));
        }).WithSummary("Atualiza mecânico");

        m.MapDelete("/mecanicos/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var mecanico = await db.Mecanicos.FirstOrDefaultAsync(m => m.Id == id);
            if (mecanico is null)
            {
                return Results.NotFound();
            }
            db.Mecanicos.Remove(mecanico);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui mecânico");

        //Fornecedores
        var f = app.MapGroup("/cadastro").WithTags("Cadastro - Fornecedores");
        
        f.MapGet("/fornecedores", async (CadastroDbContext db) =>
        {
            var fornecedores = await db.Fornecedores
                .Include(f => f.Enderecos)
                .Include(f => f.Contatos)
                .Include(f => f.Anexos)
                .Include(f => f.Bancos)
                .Include(f => f.Historicos)
                .AsNoTracking()
                .ToListAsync();
            var result = fornecedores.Select(MapToFornecedorDetalhesDto).ToList();
            return Results.Ok(result);
        }).WithSummary("Lista fornecedores");

        f.MapPost("/fornecedores", async (FornecedorCreateDto dto, CadastroDbContext db, IValidator<FornecedorCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
            {
                return Results.ValidationProblem(vr.ToDictionary());
            }

            var fornecedor = new Fornecedor
            {
                Razao_Social = dto.RazaoSocial,
                Nome_Fantasia = dto.NomeFantasia,
                Cnpj = dto.Cnpj,
                Inscricao_Estadual = dto.InscricaoEstadual,
                Contato = dto.Contato,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Observacoes = dto.Observacoes,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Ativo" : dto.Status.Trim(),
                Enderecos = dto.Enderecos?.Select(e => new FornecedorEndereco
                {
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
                }).ToList() ?? new List<FornecedorEndereco>(),
                Contatos = dto.Contatos?.Select(c => new FornecedorContato
                {
                    Tipo = c.Tipo,
                    Valor = c.Valor,
                    Principal = c.Principal,
                    Observacao = c.Observacao
                }).ToList() ?? new List<FornecedorContato>(),
                Anexos = dto.Anexos?.Select(a => new FornecedorAnexo
                {
                    Nome = a.Nome,
                    Tipo = a.Tipo,
                    Url = a.Url,
                    Observacao = a.Observacao
                }).ToList() ?? new List<FornecedorAnexo>(),
                Bancos = dto.Bancos?.Select(b => new FornecedorBanco
                {
                    Banco = b.Banco,
                    Agencia = b.Agencia,
                    Conta = b.Conta,
                    Tipo_Conta = b.TipoConta,
                    Titular = b.Titular,
                    Documento_Titular = b.DocumentoTitular,
                    Pix_Chave = b.PixChave
                }).ToList() ?? new List<FornecedorBanco>()
            };
            db.Fornecedores.Add(fornecedor);
            await db.SaveChangesAsync();
            var created = await db.Fornecedores
                .Include(f => f.Enderecos)
                .Include(f => f.Contatos)
                .Include(f => f.Anexos)
                .Include(f => f.Bancos)
                .Include(f => f.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fornecedor.Id);
            return Results.Created($"/cadastro/fornecedores/{fornecedor.Id}", MapToFornecedorDetalhesDto(created!));
        }).WithSummary("Cria fornecedor");

        f.MapGet("/fornecedores/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var fornecedor = await db.Fornecedores
                .Include(f => f.Enderecos)
                .Include(f => f.Contatos)
                .Include(f => f.Anexos)
                .Include(f => f.Bancos)
                .Include(f => f.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
            return fornecedor is null ? Results.NotFound() : Results.Ok(MapToFornecedorDetalhesDto(fornecedor));
        }).WithSummary("Obtém fornecedor por ID");

        f.MapPut("/fornecedores/{id:long}", async (long id, FornecedorCreateDto dto, CadastroDbContext db, IValidator<FornecedorCreateDto> v) =>
        {
            var vr = await v.ValidateAsync(dto);
            if (!vr.IsValid)
                return Results.ValidationProblem(vr.ToDictionary());

            var fornecedor = await db.Fornecedores
                .Include(f => f.Enderecos)
                .Include(f => f.Contatos)
                .Include(f => f.Anexos)
                .Include(f => f.Bancos)
                .Include(f => f.Historicos)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (fornecedor is null)
                return Results.NotFound();

            fornecedor.Razao_Social = dto.RazaoSocial;
            fornecedor.Nome_Fantasia = dto.NomeFantasia;
            fornecedor.Cnpj = dto.Cnpj;
            fornecedor.Inscricao_Estadual = dto.InscricaoEstadual;
            fornecedor.Contato = dto.Contato;
            fornecedor.Email = dto.Email;
            fornecedor.Telefone = dto.Telefone;
            fornecedor.Observacoes = dto.Observacoes;
            fornecedor.Status = string.IsNullOrWhiteSpace(dto.Status) ? "Ativo" : dto.Status.Trim();

            db.Set<FornecedorEndereco>().RemoveRange(fornecedor.Enderecos);
            db.Set<FornecedorContato>().RemoveRange(fornecedor.Contatos);
            db.Set<FornecedorAnexo>().RemoveRange(fornecedor.Anexos);
            db.Set<FornecedorBanco>().RemoveRange(fornecedor.Bancos);

            fornecedor.Enderecos = dto.Enderecos?.Select(e => new FornecedorEndereco
            {
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
            }).ToList() ?? new List<FornecedorEndereco>();
            fornecedor.Contatos = dto.Contatos?.Select(c => new FornecedorContato
            {
                Tipo = c.Tipo,
                Valor = c.Valor,
                Principal = c.Principal,
                Observacao = c.Observacao
            }).ToList() ?? new List<FornecedorContato>();
            fornecedor.Anexos = dto.Anexos?.Select(a => new FornecedorAnexo
            {
                Nome = a.Nome,
                Tipo = a.Tipo,
                Url = a.Url,
                Observacao = a.Observacao
            }).ToList() ?? new List<FornecedorAnexo>();
            fornecedor.Bancos = dto.Bancos?.Select(b => new FornecedorBanco
            {
                Banco = b.Banco,
                Agencia = b.Agencia,
                Conta = b.Conta,
                Tipo_Conta = b.TipoConta,
                Titular = b.Titular,
                Documento_Titular = b.DocumentoTitular,
                Pix_Chave = b.PixChave
            }).ToList() ?? new List<FornecedorBanco>();

            await db.SaveChangesAsync();
            var atualizado = await db.Fornecedores
                .Include(f => f.Enderecos)
                .Include(f => f.Contatos)
                .Include(f => f.Anexos)
                .Include(f => f.Bancos)
                .Include(f => f.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fornecedor.Id);
            return Results.Ok(MapToFornecedorDetalhesDto(atualizado!));
        }).WithSummary("Atualiza fornecedor");

        f.MapDelete("/fornecedores/{id:long}", async (long id, CadastroDbContext db) =>
        {
            var fornecedor = await db.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);
            if (fornecedor is null)
                return Results.NotFound();
            db.Fornecedores.Remove(fornecedor);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithSummary("Exclui fornecedor");
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    #region Clientes
    private static void AtualizarCliente(Cliente cliente, ClienteUpdateDto dto, CadastroDbContext db)
    {
        cliente.Nome = dto.Nome;
        cliente.NomeExibicao = dto.Nome;
        cliente.Tipo = dto.Tipo;
        cliente.Status = dto.Status;
        cliente.Origem_Id = dto.OrigemId;
        cliente.Vip = dto.Vip;
        cliente.Observacoes = dto.Observacoes;
        cliente.Documento = dto.Tipo switch
        {
            ClienteTipo.PessoaFisica => dto.PessoaFisica?.Cpf ?? cliente.Documento,
            ClienteTipo.PessoaJuridica => dto.PessoaJuridica?.Cnpj ?? cliente.Documento,
            _ => cliente.Documento
        };
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

        db.ClienteEnderecos.RemoveRange(cliente.Enderecos);
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

        db.ClienteContatos.RemoveRange(cliente.Contatos);
        cliente.Contatos = dto.Contatos?.Select(c => new ClienteContato
        {
            Cliente_Id = cliente.Id,
            Tipo = c.Tipo,
            Valor = c.Valor,
            Principal = c.Principal,
            Observacao = c.Observacao
        }).ToList() ?? new List<ClienteContato>();

        if (cliente.Contatos.FirstOrDefault(c => c.Principal) is { } contatoPrincipal)
        {
            cliente.Telefone = contatoPrincipal.Valor;
        }
        else if (cliente.Contatos.FirstOrDefault() is { } primeiroContato)
        {
            cliente.Telefone = primeiroContato.Valor;
        }

        cliente.Email = cliente.Contatos
            .Where(c => c.Tipo == ClienteContatoTipo.Email)
            .Select(c => c.Valor)
            .FirstOrDefault() ?? cliente.Email;

        if (dto.Consentimento is not null)
        {
            if (cliente.Consentimento is null)
            {
                cliente.Consentimento = new ClienteConsentimento { Cliente_Id = cliente.Id };
            }

            cliente.Consentimento.Tipo = dto.Consentimento.Tipo;
            cliente.Consentimento.Aceito = dto.Consentimento.Aceito;
            cliente.Consentimento.Data = dto.Consentimento.Data;
            cliente.Consentimento.Valido_Ate = dto.Consentimento.ValidoAte;
            cliente.Consentimento.Observacoes = dto.Consentimento.Observacoes;
            cliente.Consentimento.Canal = "API";
        }
        else if (cliente.Consentimento is not null)
        {
            db.ClienteConsentimentos.Remove(cliente.Consentimento);
            cliente.Consentimento = null;
        }

        db.VeiculosClientes.RemoveRange(cliente.Veiculos);
        cliente.Veiculos = dto.Veiculos?.Select(v => new VeiculoCliente
        {
            Cliente_Id = cliente.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo_Id = v.ModeloId,
            Ano = v.Ano,
            Cor = v.Cor,
            Chassi = v.Chassi,
            Principal = v.Principal
        }).ToList() ?? new List<VeiculoCliente>();

        db.ClienteAnexos.RemoveRange(cliente.Anexos);
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
            .Include(c => c.Consentimento)
            .Include(c => c.Veiculos)
            .Include(c => c.Anexos)
            .Include(c => c.Origem)
            .Where(c => c.Codigo == codigo);

        if (!track)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    private static async Task<Cliente?> CarregarClientePorId(CadastroDbContext db, long id)
    {
        return await db.Clientes
            .Include(c => c.PessoaFisica)
            .Include(c => c.PessoaJuridica)
            .Include(c => c.Enderecos)
            .Include(c => c.Contatos)
            .Include(c => c.Consentimento)
            .Include(c => c.Veiculos)
            .Include(c => c.Anexos)
            .Include(c => c.Origem)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    private static ClienteOrigemDto? ToOrigemDto(ClienteOrigem? origem) =>
        origem is null ? null : new ClienteOrigemDto(origem.Id, origem.Nome, origem.Descricao);

    private static ClienteDetalhesDto MapToDetalhesDto(Cliente cliente)
    {
        var pessoaFisica = cliente.PessoaFisica is null
            ? null
            : new ClientePessoaFisicaDto(
                cliente.PessoaFisica.Cpf,
                cliente.PessoaFisica.Rg,
                cliente.PessoaFisica.Data_Nascimento,
                cliente.PessoaFisica.Genero);

        var pessoaJuridica = cliente.PessoaJuridica is null
            ? null
            : new ClientePessoaJuridicaDto(
                cliente.PessoaJuridica.Cnpj,
                cliente.PessoaJuridica.Razao_Social,
                cliente.PessoaJuridica.Nome_Fantasia,
                cliente.PessoaJuridica.Inscricao_Estadual,
                cliente.PessoaJuridica.Responsavel);

        var enderecos = cliente.Enderecos
            .Select(e => new ClienteEnderecoDto(
                e.Tipo,
                e.Cep,
                e.Logradouro,
                e.Numero,
                e.Bairro,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Complemento,
                e.Principal))
            .ToList();

        var contatos = cliente.Contatos
            .Select(c => new ClienteContatoDto(c.Tipo, c.Valor, c.Principal, c.Observacao))
            .ToList();

        var consentimentos = cliente.Consentimento is null
            ? Array.Empty<ClienteConsentimentoDto>()
            : new[]
            {
                new ClienteConsentimentoDto(
                    cliente.Consentimento.Tipo,
                    cliente.Consentimento.Aceito,
                    cliente.Consentimento.Data,
                    cliente.Consentimento.Valido_Ate,
                    cliente.Consentimento.Observacoes)
            };

        var veiculos = cliente.Veiculos
            .Select(v => new ClienteVeiculoDto(
                v.Placa,
                v.Marca,
                v.Modelo_Id,
                v.Modelo?.Nome,
                v.Ano,
                v.Cor,
                v.Chassi,
                v.Principal))
            .ToList();

        var anexos = cliente.Anexos
            .Select(a => new ClienteAnexoDto(a.Nome, a.Tipo, a.Url, a.Observacao))
            .ToList();

        return new ClienteDetalhesDto(
            cliente.Codigo,
            cliente.Id,
            cliente.Nome,
            cliente.Tipo,
            cliente.Status,
            ToOrigemDto(cliente.Origem),
            cliente.Vip,
            cliente.Observacoes,
            pessoaFisica,
            pessoaJuridica,
            enderecos,
            contatos,
            consentimentos,
            veiculos,
            anexos);
    }

    private static Cliente MapToEntity(ClienteCreateDto dto)
    {
        var cliente = new Cliente
        {
            Nome = dto.Nome,
            NomeExibicao = dto.Nome,
            Tipo = dto.Tipo,
            Status = dto.Status,
            Origem_Id = dto.OrigemId,
            Vip = dto.Vip,
            Observacoes = dto.Observacoes,
            Telefone = string.Empty,
            Email = string.Empty
        };

        cliente.Documento = dto.Tipo switch
        {
            ClienteTipo.PessoaFisica => dto.PessoaFisica?.Cpf ?? string.Empty,
            ClienteTipo.PessoaJuridica => dto.PessoaJuridica?.Cnpj ?? string.Empty,
            _ => string.Empty
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

        if (cliente.Contatos.FirstOrDefault(c => c.Principal) is { } contatoPrincipal)
        {
            cliente.Telefone = contatoPrincipal.Valor;
        }
        else if (cliente.Contatos.FirstOrDefault() is { } primeiroContato)
        {
            cliente.Telefone = primeiroContato.Valor;
        }

        cliente.Email = cliente.Contatos
            .Where(c => c.Tipo == ClienteContatoTipo.Email)
            .Select(c => c.Valor)
            .FirstOrDefault() ?? cliente.Email;

        if (dto.Consentimentos?.FirstOrDefault() is { } consentimento)
        {
            cliente.Consentimento = new ClienteConsentimento
            {
                Cliente_Id = cliente.Id,
                Tipo = consentimento.Tipo,
                Aceito = consentimento.Aceito,
                Data = consentimento.Data,
                Valido_Ate = consentimento.ValidoAte,
                Observacoes = consentimento.Observacoes,
                Canal = "API"
            };
        }

        cliente.Veiculos = dto.Veiculos?.Select(v => new VeiculoCliente
        {
            Cliente_Id = cliente.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo_Id = v.ModeloId,
            Ano = v.Ano,
            Cor = v.Cor,
            Chassi = v.Chassi,
            Principal = v.Principal
        }).ToList() ?? new List<VeiculoCliente>();

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
    #endregion

    #region Mecanicos
    private static MecanicoResumoDto MapToMecanicoResumoDto(Mecanico mecanico)
    {
        return new MecanicoResumoDto(
            mecanico.Id,
            mecanico.Codigo,
            ComposeNomeCompleto(mecanico),
            mecanico.Status,
            mecanico.Nivel,
            mecanico.EspecialidadePrincipal?.Nome,
            mecanico.Valor_Hora);
    }

    private static string ComposeNomeCompleto(Mecanico mecanico) =>
        string.IsNullOrWhiteSpace(mecanico.Sobrenome)
            ? mecanico.Nome
            : $"{mecanico.Nome} {mecanico.Sobrenome}".Trim();

    private static async Task<Mecanico?> CarregarMecanicoCompleto(CadastroDbContext db, long mecanicoId)
    {
        return await db.Mecanicos
            .Include(m => m.EspecialidadePrincipal)
            .Include(m => m.Especialidades).ThenInclude(e => e.Especialidade)
            .Include(m => m.Contatos)
            .Include(m => m.Enderecos)
            .Include(m => m.Documentos)
            .Include(m => m.Certificacoes).ThenInclude(c => c.Especialidade)
            .Include(m => m.Disponibilidades)
            .Include(m => m.Experiencias)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == mecanicoId);
    }

    private static Mecanico MapToMecanicoEntity(MecanicoCreateDto dto)
    {
        var mecanico = new Mecanico
        {
            Codigo = dto.Codigo.Trim(),
            Nome = dto.Nome.Trim(),
            Sobrenome = Normalize(dto.Sobrenome),
            Nome_Social = Normalize(dto.NomeSocial),
            Documento_Principal = dto.DocumentoPrincipal.Trim(),
            Tipo_Documento = dto.TipoDocumento,
            Data_Admissao = dto.DataAdmissao,
            Data_Nascimento = dto.DataNascimento,
            Data_Demissao = dto.DataDemissao,
            Status = string.IsNullOrWhiteSpace(dto.Status) ? "Ativo" : dto.Status.Trim(),
            Especialidade_Principal_Id = dto.EspecialidadePrincipalId,
            Nivel = string.IsNullOrWhiteSpace(dto.Nivel) ? "Pleno" : dto.Nivel.Trim(),
            Valor_Hora = dto.ValorHora,
            Carga_Horaria_Semanal = dto.CargaHorariaSemanal,
            Observacoes = Normalize(dto.Observacoes)
        };

        if (dto.Especialidades is { Count: > 0 })
        {
            mecanico.Especialidades = dto.Especialidades
                .Select(e => new MecanicoEspecialidadeRel
                {
                    Especialidade_Id = e.EspecialidadeId,
                    Nivel = e.Nivel.Trim(),
                    Principal = e.Principal,
                    Anotacoes = Normalize(e.Anotacoes)
                })
                .ToList();
        }

        if (dto.Contatos is { Count: > 0 })
        {
            mecanico.Contatos = dto.Contatos
                .Select(c => new MecanicoContato
                {
                    Tipo = c.Tipo.Trim(),
                    Valor = c.Valor.Trim(),
                    Principal = c.Principal,
                    Observacao = Normalize(c.Observacao)
                })
                .ToList();
        }

        if (dto.Enderecos is { Count: > 0 })
        {
            mecanico.Enderecos = dto.Enderecos
                .Select(e => new MecanicoEndereco
                {
                    Tipo = e.Tipo.Trim(),
                    Cep = e.Cep.Trim(),
                    Logradouro = e.Logradouro.Trim(),
                    Numero = e.Numero.Trim(),
                    Bairro = e.Bairro.Trim(),
                    Cidade = e.Cidade.Trim(),
                    Estado = e.Estado.Trim(),
                    Pais = Normalize(e.Pais),
                    Complemento = Normalize(e.Complemento),
                    Principal = e.Principal
                })
                .ToList();
        }

        if (dto.Documentos is { Count: > 0 })
        {
            mecanico.Documentos = dto.Documentos
                .Select(d => new MecanicoDocumento
                {
                    Tipo = d.Tipo.Trim(),
                    Numero = d.Numero.Trim(),
                    Data_Emissao = d.DataEmissao,
                    Data_Validade = d.DataValidade,
                    Orgao_Expedidor = Normalize(d.OrgaoExpedidor),
                    Arquivo_Url = Normalize(d.ArquivoUrl)
                })
                .ToList();
        }

        if (dto.Certificacoes is { Count: > 0 })
        {
            mecanico.Certificacoes = dto.Certificacoes
                .Select(c => new MecanicoCertificacao
                {
                    Especialidade_Id = c.EspecialidadeId,
                    Titulo = c.Titulo.Trim(),
                    Instituicao = Normalize(c.Instituicao),
                    Data_Conclusao = c.DataConclusao,
                    Data_Validade = c.DataValidade,
                    Codigo_Certificacao = Normalize(c.CodigoCertificacao)
                })
                .ToList();
        }

        if (dto.Disponibilidades is { Count: > 0 })
        {
            mecanico.Disponibilidades = dto.Disponibilidades
                .Select(d => new MecanicoDisponibilidade
                {
                    Dia_Semana = d.DiaSemana,
                    Hora_Inicio = d.HoraInicio,
                    Hora_Fim = d.HoraFim,
                    Capacidade_Atendimentos = d.CapacidadeAtendimentos
                })
                .ToList();
        }

        if (dto.Experiencias is { Count: > 0 })
        {
            mecanico.Experiencias = dto.Experiencias
                .Select(e => new MecanicoExperiencia
                {
                    Empresa = e.Empresa.Trim(),
                    Cargo = e.Cargo.Trim(),
                    Data_Inicio = e.DataInicio,
                    Data_Fim = e.DataFim,
                    Resumo_Atividades = Normalize(e.ResumoAtividades)
                })
                .ToList();
        }

        return mecanico;
    }

    private static MecanicoDetalhesDto MapToMecanicoDetalhesDto(Mecanico mecanico)
    {
        var especialidades = mecanico.Especialidades?
            .OrderByDescending(e => e.Principal)
            .ThenBy(e => e.Nivel)
            .Select(e => new MecanicoEspecialidadeResumoDto(
                e.Especialidade_Id,
                e.Especialidade?.Codigo,
                e.Especialidade?.Nome,
                e.Nivel,
                e.Principal,
                e.Anotacoes))
            .ToList() ?? new List<MecanicoEspecialidadeResumoDto>();

        var contatos = mecanico.Contatos?
            .Select(c => new MecanicoContatoResumoDto(c.Id, c.Tipo, c.Valor, c.Principal, c.Observacao))
            .ToList() ?? new List<MecanicoContatoResumoDto>();

        var enderecos = mecanico.Enderecos?
            .Select(e => new MecanicoEnderecoResumoDto(
                e.Id,
                e.Tipo,
                e.Cep,
                e.Logradouro,
                e.Numero,
                e.Bairro,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Complemento,
                e.Principal))
            .ToList() ?? new List<MecanicoEnderecoResumoDto>();

        var documentos = mecanico.Documentos?
            .Select(d => new MecanicoDocumentoResumoDto(
                d.Id,
                d.Tipo,
                d.Numero,
                d.Data_Emissao,
                d.Data_Validade,
                d.Orgao_Expedidor,
                d.Arquivo_Url))
            .ToList() ?? new List<MecanicoDocumentoResumoDto>();

        var certificacoes = mecanico.Certificacoes?
            .Select(c => new MecanicoCertificacaoResumoDto(
                c.Id,
                c.Especialidade_Id,
                c.Titulo,
                c.Instituicao,
                c.Data_Conclusao,
                c.Data_Validade,
                c.Codigo_Certificacao,
                c.Especialidade?.Nome))
            .ToList() ?? new List<MecanicoCertificacaoResumoDto>();

        var disponibilidades = mecanico.Disponibilidades?
            .OrderBy(d => d.Dia_Semana)
            .ThenBy(d => d.Hora_Inicio)
            .Select(d => new MecanicoDisponibilidadeResumoDto(
                d.Id,
                d.Dia_Semana,
                d.Hora_Inicio,
                d.Hora_Fim,
                d.Capacidade_Atendimentos))
            .ToList() ?? new List<MecanicoDisponibilidadeResumoDto>();

        var experiencias = mecanico.Experiencias?
            .OrderByDescending(e => e.Data_Inicio ?? DateTime.MinValue)
            .Select(e => new MecanicoExperienciaResumoDto(
                e.Id,
                e.Empresa,
                e.Cargo,
                e.Data_Inicio,
                e.Data_Fim,
                e.Resumo_Atividades))
            .ToList() ?? new List<MecanicoExperienciaResumoDto>();

        return new MecanicoDetalhesDto(
            mecanico.Id,
            mecanico.Codigo,
            mecanico.Nome,
            mecanico.Sobrenome,
            mecanico.Nome_Social,
            mecanico.Documento_Principal,
            mecanico.Tipo_Documento,
            mecanico.Data_Admissao,
            mecanico.Data_Nascimento,
            mecanico.Data_Demissao,
            mecanico.Status,
            mecanico.Especialidade_Principal_Id,
            mecanico.EspecialidadePrincipal?.Nome,
            mecanico.Nivel,
            mecanico.Valor_Hora,
            mecanico.Carga_Horaria_Semanal,
            mecanico.Observacoes,
            especialidades,
            contatos,
            enderecos,
            documentos,
            certificacoes,
            disponibilidades,
            experiencias);
    }

    #endregion

    #region Fornecedores
    private static FornecedorDetalhesDto MapToFornecedorDetalhesDto(Fornecedor fornecedor)
    {
        return new FornecedorDetalhesDto(
            fornecedor.Id,
            fornecedor.Razao_Social,
            fornecedor.Nome_Fantasia,
            fornecedor.Cnpj,
            fornecedor.Inscricao_Estadual,
            fornecedor.Contato,
            fornecedor.Email,
            fornecedor.Telefone,
            fornecedor.Observacoes,
            fornecedor.Status,
            fornecedor.Enderecos.Select(e => new FornecedorEnderecoDto(
                e.Tipo,
                e.Cep,
                e.Logradouro,
                e.Numero,
                e.Bairro,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Complemento,
                e.Principal
            )).ToList(),
            fornecedor.Contatos.Select(c => new FornecedorContatoDto(
                c.Tipo,
                c.Valor,
                c.Principal,
                c.Observacao
            )).ToList(),
            fornecedor.Anexos.Select(a => new FornecedorAnexoDto(
                a.Nome,
                a.Tipo,
                a.Url,
                a.Observacao
            )).ToList(),
            fornecedor.Bancos.Select(b => new FornecedorBancoDto(
                b.Banco,
                b.Agencia,
                b.Conta,
                b.Tipo_Conta,
                b.Titular,
                b.Documento_Titular,
                b.Pix_Chave
            )).ToList(),
            fornecedor.Historicos.Select(h => new FornecedorHistoricoDto(
                h.Data_Alteracao,
                h.Usuario,
                h.Campo,
                h.Valor_Antigo,
                h.Valor_Novo
            )).ToList()
        );
    }
    #endregion
}

































































































































