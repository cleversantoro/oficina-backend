using System;
using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class VeiculoModelo : Entity
{
    public Guid Marca_Id { get; set; }
    public VeiculoMarca Marca { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int? Ano_Inicio { get; set; }
    public int? Ano_Fim { get; set; }
    public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
