using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteVeiculo : Entity
{
    public Guid Cliente_Id { get; set; }
    public string Placa { get; set; } = default!;
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public int? Ano { get; set; }
    public string? Cor { get; set; }
    public string? Chassi { get; set; }
    public bool Principal { get; set; }
}
