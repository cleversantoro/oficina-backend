using FluentValidation;
namespace Oficina.Financeiro.Api;
public class PagamentoCreateValidator : AbstractValidator<PagamentoCreateDto>
{
    public PagamentoCreateValidator()
    {
        RuleFor(x=>x.OrdemServicoId).NotEmpty();
        RuleFor(x=>x.Meio).Must(m=>new[]{"PIX","BOLETO","CARTAO"}.Contains(m.ToUpper())).WithMessage("Meio deve ser PIX, BOLETO ou CARTAO");
        RuleFor(x=>x.Valor).GreaterThan(0);
    }
}
public class AtualizaStatusValidator : AbstractValidator<AtualizaStatusDto>
{
    public AtualizaStatusValidator(){ RuleFor(x=>x.Status).NotEmpty(); }
}
public class NfeCreateValidator : AbstractValidator<NfeCreateDto>
{
    public NfeCreateValidator(){ RuleFor(x=>x.OrdemServicoId).NotEmpty(); }
}
