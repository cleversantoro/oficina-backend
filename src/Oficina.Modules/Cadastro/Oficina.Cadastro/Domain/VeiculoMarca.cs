using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class VeiculoMarca : Entity
{
    public string Nome { get; set; } = default!;
    public string? Pais { get; set; }
    public ICollection<VeiculoModelo> Modelos { get; set; } = new List<VeiculoModelo>();
}

