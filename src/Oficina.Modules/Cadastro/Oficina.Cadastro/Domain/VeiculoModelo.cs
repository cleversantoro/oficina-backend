using System;
using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class VeiculoModelo : Entity
{
    public long Marca_Id { get; set; }
    public VeiculoMarca Marca { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int? Ano_Inicio { get; set; }
    public int? Ano_Fim { get; set; }
    public ICollection<VeiculoCliente> Veiculos { get; set; } = new List<VeiculoCliente>();
}

