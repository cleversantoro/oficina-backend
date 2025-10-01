using FluentValidation;
namespace Oficina.Cadastro.Api;
public class ClienteCreateValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateValidator()
    {
        RuleFor(x=>x.Nome).NotEmpty().MaximumLength(120);
        RuleFor(x=>x.Documento).NotEmpty().MaximumLength(20);
        RuleFor(x=>x.Email).EmailAddress().When(x=>!string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x=>x.Telefone).NotEmpty();
    }
}
public class MecanicoCreateValidator : AbstractValidator<MecanicoCreateDto>
{
    public MecanicoCreateValidator() { RuleFor(x=>x.Nome).NotEmpty().MaximumLength(120); }
}
public class FornecedorCreateValidator : AbstractValidator<FornecedorCreateDto>
{
    public FornecedorCreateValidator()
    {
        RuleFor(x=>x.RazaoSocial).NotEmpty().MaximumLength(160);
        RuleFor(x=>x.Cnpj).NotEmpty().MaximumLength(20);
        RuleFor(x=>x.Contato).NotEmpty();
    }
}
