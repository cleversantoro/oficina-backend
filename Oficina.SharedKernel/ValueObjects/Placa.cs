using System;
using System.Text.RegularExpressions;

namespace Oficina.SharedKernel.ValueObjects;

public record Placa
{
    public string Value { get; }

    // aceita AAA0A00 (Mercosul) e AAA0000 (antiga)
    private static readonly Regex Rgx = new(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$|^[A-Z]{3}[0-9]{4}$", RegexOptions.IgnoreCase);

    public Placa(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Placa obrigatória");
        var norm = value.ToUpper().Replace("-", "").Trim();
        if (!Rgx.IsMatch(norm)) throw new ArgumentException("Placa inválida");
        Value = norm;
    }

    public static implicit operator string(Placa p) => p.Value;
    public override string ToString() => Value;
}
