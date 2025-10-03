using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class Veiculo : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public Guid Modelo_Id { get; set; }
    public VeiculoModelo Modelo { get; set; } = default!;
    public string Placa { get; set; } = default!;
    public int? Ano_Fabricacao { get; set; }
    public int? Ano_Modelo { get; set; }
    public string? Cor { get; set; }
    public string? Renavam { get; set; }
    public string? Chassi { get; set; }
    public string? Combustivel { get; set; }
    public string? Observacao { get; set; }
}
