using FluentValidation;
namespace Oficina.OrdemServico.Api;
public class OrdemCreateValidator : AbstractValidator<OrdemCreateDto>
{
    public OrdemCreateValidator()
    {
        RuleFor(x=>x.ClienteId).NotEmpty();
        RuleFor(x=>x.MecanicoId).NotEmpty();
        RuleFor(x=>x.DescricaoProblema).NotEmpty().MaximumLength(400);
    }
}
public class ItemCreateValidator : AbstractValidator<ItemCreateDto>
{
    public ItemCreateValidator()
    {
        RuleFor(x=>x.Descricao).NotEmpty();
        RuleFor(x=>x.Quantidade).GreaterThan(0);
        RuleFor(x=>x.ValorUnitario).GreaterThanOrEqualTo(0);
    }
}
