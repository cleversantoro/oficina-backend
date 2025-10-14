using System;
using System.Linq;
using FluentValidation;
using Oficina.Cadastro.Domain;

namespace Oficina.Cadastro.Api;

public class ClientePessoaFisicaDtoValidator : AbstractValidator<ClientePessoaFisicaDto>
{
    public ClientePessoaFisicaDtoValidator()
    {
        RuleFor(x => x.Cpf).NotEmpty().Length(11, 14).Matches("^\\d{11}$").WithMessage("CPF deve conter 11 dÃ­gitos");
        RuleFor(x => x.Rg).MaximumLength(20);
        RuleFor(x => x.Genero).MaximumLength(30);
    }
}

public class ClientePessoaJuridicaDtoValidator : AbstractValidator<ClientePessoaJuridicaDto>
{
    public ClientePessoaJuridicaDtoValidator()
    {
        RuleFor(x => x.Cnpj).NotEmpty().Length(14, 18).Matches("^\\d{14}$").WithMessage("CNPJ deve conter 14 dÃ­gitos");
        RuleFor(x => x.RazaoSocial).NotEmpty().MaximumLength(180);
        RuleFor(x => x.NomeFantasia).MaximumLength(180);
        RuleFor(x => x.InscricaoEstadual).MaximumLength(30);
        RuleFor(x => x.Responsavel).MaximumLength(120);
    }
}

public class ClienteEnderecoDtoValidator : AbstractValidator<ClienteEnderecoDto>
{
    public ClienteEnderecoDtoValidator()
    {
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Cep).NotEmpty().MaximumLength(12);
        RuleFor(x => x.Logradouro).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Numero).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Bairro).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Cidade).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Estado).NotEmpty().MaximumLength(60);
        RuleFor(x => x.Pais).NotEmpty().MaximumLength(60);
        RuleFor(x => x.Complemento).MaximumLength(160);
    }
}

public class ClienteContatoDtoValidator : AbstractValidator<ClienteContatoDto>
{
    public ClienteContatoDtoValidator()
    {
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Valor).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Observacao).MaximumLength(200);
    }
}

public class ClienteConsentimentoDtoValidator : AbstractValidator<ClienteConsentimentoDto>
{
    public ClienteConsentimentoDtoValidator()
    {
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Observacoes).MaximumLength(200);
    }
}

public class ClienteVeiculoDtoValidator : AbstractValidator<ClienteVeiculoDto>
{
    public ClienteVeiculoDtoValidator()
    {
        RuleFor(x => x.Placa).NotEmpty().MaximumLength(10);
        RuleFor(x => x.Marca).MaximumLength(80);
        RuleFor(x => x.ModeloNome).MaximumLength(160);
        RuleFor(x => x.ModeloId).GreaterThan(0).When(x => x.ModeloId.HasValue);
        RuleFor(x => x.Cor).MaximumLength(60);
        RuleFor(x => x.Chassi).MaximumLength(30);
        RuleFor(x => x.Ano).InclusiveBetween(1900, DateTime.UtcNow.Year + 1).When(x => x.Ano.HasValue);
    }
}

public class ClienteAnexoDtoValidator : AbstractValidator<ClienteAnexoDto>
{
    public ClienteAnexoDtoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(300);
        RuleFor(x => x.Observacao).MaximumLength(200);
    }
}

public class ClienteCreateValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(180);
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.OrigemId).GreaterThan(0);
        RuleFor(x => x.Observacoes).MaximumLength(500);

        RuleFor(x => x.PessoaFisica)
            .NotNull().WithMessage("Dados de pessoa fÃ­sica sÃ£o obrigatÃ³rios para clientes PF.")
            .When(x => x.Tipo == ClienteTipo.PessoaFisica);
        RuleFor(x => x.PessoaFisica!)
            .SetValidator(new ClientePessoaFisicaDtoValidator())
            .When(x => x.Tipo == ClienteTipo.PessoaFisica && x.PessoaFisica is not null);

        RuleFor(x => x.PessoaJuridica)
            .NotNull().WithMessage("Dados de pessoa jurÃ­dica sÃ£o obrigatÃ³rios para clientes PJ.")
            .When(x => x.Tipo == ClienteTipo.PessoaJuridica);
        RuleFor(x => x.PessoaJuridica!)
            .SetValidator(new ClientePessoaJuridicaDtoValidator())
            .When(x => x.Tipo == ClienteTipo.PessoaJuridica && x.PessoaJuridica is not null);

        When(x => x.Enderecos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Enderecos!).Must(TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um endereÃ§o principal.");
            RuleForEach(x => x.Enderecos!).SetValidator(new ClienteEnderecoDtoValidator());
        });

        When(x => x.Contatos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Contatos!).Must(TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um contato principal.");
            RuleForEach(x => x.Contatos!).SetValidator(new ClienteContatoDtoValidator());
        });

        When(x => x.Consentimentos is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Consentimentos!).SetValidator(new ClienteConsentimentoDtoValidator());
        });

        When(x => x.Veiculos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Veiculos!).Must(TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um veÃ­culo principal.");
            RuleForEach(x => x.Veiculos!).SetValidator(new ClienteVeiculoDtoValidator());
        });

        When(x => x.Anexos is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Anexos!).SetValidator(new ClienteAnexoDtoValidator());
        });
    }

    internal static bool TemApenasUmPrincipal<T>(IEnumerable<T> itens) where T : notnull
    {
        var principalProperty = typeof(T).GetProperty("Principal");
        if (principalProperty is null)
        {
            return true;
        }

        return itens.Count(item => principalProperty.GetValue(item) is true) <= 1;
    }
}

public class ClienteUpdateValidator : AbstractValidator<ClienteUpdateDto>
{
    public ClienteUpdateValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(180);
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.OrigemId).GreaterThan(0);
        RuleFor(x => x.Observacoes).MaximumLength(500);

        RuleFor(x => x.PessoaFisica)
            .NotNull().WithMessage("Dados de pessoa fÃ­sica sÃ£o obrigatÃ³rios para clientes PF.")
            .When(x => x.Tipo == ClienteTipo.PessoaFisica);
        RuleFor(x => x.PessoaFisica!)
            .SetValidator(new ClientePessoaFisicaDtoValidator())
            .When(x => x.Tipo == ClienteTipo.PessoaFisica && x.PessoaFisica is not null);

        RuleFor(x => x.PessoaJuridica)
            .NotNull().WithMessage("Dados de pessoa jurÃ­dica sÃ£o obrigatÃ³rios para clientes PJ.")
            .When(x => x.Tipo == ClienteTipo.PessoaJuridica);
        RuleFor(x => x.PessoaJuridica!)
            .SetValidator(new ClientePessoaJuridicaDtoValidator())
            .When(x => x.Tipo == ClienteTipo.PessoaJuridica && x.PessoaJuridica is not null);

        When(x => x.Enderecos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Enderecos!).Must(ClienteCreateValidator.TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um endereÃ§o principal.");
            RuleForEach(x => x.Enderecos!).SetValidator(new ClienteEnderecoDtoValidator());
        });

        When(x => x.Contatos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Contatos!).Must(ClienteCreateValidator.TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um contato principal.");
            RuleForEach(x => x.Contatos!).SetValidator(new ClienteContatoDtoValidator());
        });

        //When(x => x.Consentimentos is { Count: > 0 }, () =>
        //{
        //    RuleForEach(x => x.Consentimentos!).SetValidator(new ClienteConsentimentoDtoValidator());
        //});

        When(x => x.Veiculos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Veiculos!).Must(ClienteCreateValidator.TemApenasUmPrincipal).WithMessage("Ã‰ permitido somente um veÃ­culo principal.");
            RuleForEach(x => x.Veiculos!).SetValidator(new ClienteVeiculoDtoValidator());
        });

        When(x => x.Anexos is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Anexos!).SetValidator(new ClienteAnexoDtoValidator());
        });
    }
}

public class MecanicoEspecialidadeRelDtoValidator : AbstractValidator<MecanicoEspecialidadeRelDto>
{
    public MecanicoEspecialidadeRelDtoValidator()
    {
        RuleFor(x => x.EspecialidadeId).GreaterThan(0);
        RuleFor(x => x.Nivel).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Anotacoes).MaximumLength(240);
    }
}

public class MecanicoContatoDtoValidator : AbstractValidator<MecanicoContatoDto>
{
    public MecanicoContatoDtoValidator()
    {
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Valor).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Observacao).MaximumLength(240);
    }
}

public class MecanicoEnderecoDtoValidator : AbstractValidator<MecanicoEnderecoDto>
{
    public MecanicoEnderecoDtoValidator()
    {
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Cep).NotEmpty().MaximumLength(12);
        RuleFor(x => x.Logradouro).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Numero).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Bairro).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Cidade).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Estado).NotEmpty().MaximumLength(60);
        RuleFor(x => x.Pais).MaximumLength(80);
        RuleFor(x => x.Complemento).MaximumLength(120);
    }
}

public class MecanicoDocumentoDtoValidator : AbstractValidator<MecanicoDocumentoDto>
{
    public MecanicoDocumentoDtoValidator()
    {
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Numero).NotEmpty().MaximumLength(40);
        RuleFor(x => x.OrgaoExpedidor).MaximumLength(80);
        RuleFor(x => x.ArquivoUrl).MaximumLength(240);
        RuleFor(x => x.DataValidade)
            .GreaterThanOrEqualTo(x => x.DataEmissao!.Value)
            .When(x => x.DataValidade.HasValue && x.DataEmissao.HasValue);
    }
}

public class MecanicoCertificacaoDtoValidator : AbstractValidator<MecanicoCertificacaoDto>
{
    public MecanicoCertificacaoDtoValidator()
    {
        RuleFor(x => x.EspecialidadeId).GreaterThan(0).When(x => x.EspecialidadeId.HasValue);
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Instituicao).MaximumLength(160);
        RuleFor(x => x.CodigoCertificacao).MaximumLength(60);
        RuleFor(x => x.DataValidade)
            .GreaterThanOrEqualTo(x => x.DataConclusao!.Value)
            .When(x => x.DataValidade.HasValue && x.DataConclusao.HasValue);
    }
}

public class MecanicoDisponibilidadeDtoValidator : AbstractValidator<MecanicoDisponibilidadeDto>
{
    public MecanicoDisponibilidadeDtoValidator()
    {
        RuleFor(x => x.DiaSemana).InclusiveBetween((byte)0, (byte)6);
        RuleFor(x => x.CapacidadeAtendimentos).GreaterThan(0);
        RuleFor(x => x.HoraFim)
            .GreaterThan(x => x.HoraInicio)
            .WithMessage("Hora fim deve ser maior que hora início.");
    }
}

public class MecanicoExperienciaDtoValidator : AbstractValidator<MecanicoExperienciaDto>
{
    public MecanicoExperienciaDtoValidator()
    {
        RuleFor(x => x.Empresa).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Cargo).NotEmpty().MaximumLength(120);
        RuleFor(x => x.ResumoAtividades).MaximumLength(400);
        RuleFor(x => x.DataFim)
            .GreaterThanOrEqualTo(x => x.DataInicio!.Value)
            .When(x => x.DataFim.HasValue && x.DataInicio.HasValue);
    }
}

public class MecanicoCreateValidator : AbstractValidator<MecanicoCreateDto>
{
    public MecanicoCreateValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Sobrenome).MaximumLength(120);
        RuleFor(x => x.NomeSocial).MaximumLength(120);
        RuleFor(x => x.DocumentoPrincipal).NotEmpty().MaximumLength(20);
        RuleFor(x => x.TipoDocumento).GreaterThan(0);
        RuleFor(x => x.DataAdmissao).Must(d => d != default).WithMessage("Data de admissão é obrigatória.");
        RuleFor(x => x.DataDemissao)
            .GreaterThanOrEqualTo(x => x.DataAdmissao)
            .When(x => x.DataDemissao.HasValue);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Nivel).NotEmpty().MaximumLength(20);
        RuleFor(x => x.ValorHora).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CargaHorariaSemanal).InclusiveBetween(1, 80);
        RuleFor(x => x.Observacoes).MaximumLength(500);

        When(x => x.Especialidades is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Especialidades!)
                .Must(ClienteCreateValidator.TemApenasUmPrincipal)
                .WithMessage("É permitido somente uma especialidade principal.");
            RuleForEach(x => x.Especialidades!).SetValidator(new MecanicoEspecialidadeRelDtoValidator());
        });

        RuleFor(x => x)
            .Must(dto => !dto.EspecialidadePrincipalId.HasValue
                         || dto.Especialidades is { Count: > 0 }
                            && dto.Especialidades.Any(e => e.EspecialidadeId == dto.EspecialidadePrincipalId && e.Principal))
            .WithMessage("Especialidade principal deve estar presente na lista de especialidades como principal.");

        When(x => x.Contatos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Contatos!).Must(ClienteCreateValidator.TemApenasUmPrincipal).WithMessage("É permitido somente um contato principal.");
            RuleForEach(x => x.Contatos!).SetValidator(new MecanicoContatoDtoValidator());
        });

        When(x => x.Enderecos is { Count: > 0 }, () =>
        {
            RuleFor(x => x.Enderecos!).Must(ClienteCreateValidator.TemApenasUmPrincipal).WithMessage("É permitido somente um endereço principal.");
            RuleForEach(x => x.Enderecos!).SetValidator(new MecanicoEnderecoDtoValidator());
        });

        When(x => x.Documentos is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Documentos!).SetValidator(new MecanicoDocumentoDtoValidator());
        });

        When(x => x.Certificacoes is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Certificacoes!).SetValidator(new MecanicoCertificacaoDtoValidator());
        });

        When(x => x.Disponibilidades is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Disponibilidades!).SetValidator(new MecanicoDisponibilidadeDtoValidator());
        });

        When(x => x.Experiencias is { Count: > 0 }, () =>
        {
            RuleForEach(x => x.Experiencias!).SetValidator(new MecanicoExperienciaDtoValidator());
        });
    }
}

public class FornecedorCreateValidator : AbstractValidator<FornecedorCreateDto>
{
    public FornecedorCreateValidator()
    {
        RuleFor(x => x.RazaoSocial).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Cnpj).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Contato).NotEmpty();
    }
}


