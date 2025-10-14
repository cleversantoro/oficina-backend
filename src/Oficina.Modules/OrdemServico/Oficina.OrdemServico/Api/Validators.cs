﻿using FluentValidation;
namespace Oficina.OrdemServico.Api;
public class OrdemCreateValidator : AbstractValidator<OrdemCreateDto>
{
    public OrdemCreateValidator()
    {
        RuleFor(x=>x.ClienteId).GreaterThan(0);
        RuleFor(x=>x.MecanicoId).GreaterThan(0);
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
        RuleFor(x=>x.PecaId).GreaterThan(0).When(x=>x.PecaId.HasValue);
    }
}

