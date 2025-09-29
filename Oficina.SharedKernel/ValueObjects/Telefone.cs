using System;
using System.Linq;

namespace Oficina.SharedKernel.ValueObjects;

public record Telefone
{
    public string Value { get; }

    public Telefone(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Telefone obrigatório");
        Value = new string(value.Where(char.IsDigit).ToArray());
        if (Value.Length < 10 || Value.Length > 12) throw new ArgumentException("Telefone inválido");
    }

    public static implicit operator string(Telefone t) => t.Value;
    public override string ToString() => Value;
}
