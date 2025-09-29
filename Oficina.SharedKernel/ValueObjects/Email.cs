using System;

namespace Oficina.SharedKernel.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email obrigatório");
        if (!value.Contains("@")) throw new ArgumentException("Email inválido");
        Value = value.Trim();
    }

    public static implicit operator string(Email e) => e.Value;
    public override string ToString() => Value;
}
