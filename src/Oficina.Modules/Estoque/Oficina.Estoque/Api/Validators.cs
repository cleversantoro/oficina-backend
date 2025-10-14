using FluentValidation;
namespace Oficina.Estoque.Api;
public class PecaCreateValidator : AbstractValidator<PecaCreateDto>
{
    public PecaCreateValidator()
    {
        RuleFor(x=>x.Codigo).NotEmpty().MaximumLength(40);
        RuleFor(x=>x.Descricao).NotEmpty().MaximumLength(200);
        RuleFor(x=>x.PrecoUnitario).GreaterThanOrEqualTo(0);
        RuleFor(x=>x.Quantidade).GreaterThanOrEqualTo(0);
    }
}
public class MovimentacaoCreateValidator : AbstractValidator<MovimentacaoCreateDto>
{
    public MovimentacaoCreateValidator()
    {
        RuleFor(x=>x.PecaId).NotEmpty();
        RuleFor(x=>x.Quantidade).GreaterThan(0);
        RuleFor(x=>x.Tipo).Must(t=>new[]{"ENTRADA","SAIDA"}.Contains(t.ToUpper())).WithMessage("Tipo deve ser ENTRADA ou SAIDA");
    }
}

