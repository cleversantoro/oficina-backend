using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteVeiculo : Entity
{
    public long Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Placa { get; set; } = default!;
    public string? Marca { get; set; }
    public int? Ano { get; set; }
    public string? Cor { get; set; }
    public string? Chassi { get; set; }
    public bool Principal { get; set; }
    public long? Modelo_Id { get; set; }
    public VeiculoModelo? Modelo { get; set; }
    public string? Renavam { get; set; }
    public string? Combustivel { get; set; }
    public string? Observacao { get; set; }
}
