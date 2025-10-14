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

public class MecanicoCreateValidator : AbstractValidator<MecanicoCreateDto>
{
    public MecanicoCreateValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(120);
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

