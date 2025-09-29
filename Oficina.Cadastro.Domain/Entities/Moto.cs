namespace Oficina.Cadastro.Domain.Entities;

using Oficina.SharedKernel.ValueObjects;

public class Moto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ClienteId { get; private set; }
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
    public int Ano { get; private set; }
    public Placa Placa { get; private set; }
    public string? Chassi { get; private set; }
    public int? KmAtual { get; private set; }

    public Cliente? Cliente { get; private set; }

    //private Moto() { }

    public Moto(Guid clienteId, string marca, string modelo, int ano, Placa placa, string? chassi, int? kmAtual)
    {
        if (clienteId == Guid.Empty) throw new ArgumentException("ClienteId inválido");
        ClienteId = clienteId;

        Marca = string.IsNullOrWhiteSpace(marca) ? throw new ArgumentException("Marca obrigatória") : marca.Trim();
        Modelo = string.IsNullOrWhiteSpace(modelo) ? throw new ArgumentException("Modelo obrigatório") : modelo.Trim();
        Ano = ano;
        Placa = placa ?? throw new ArgumentNullException(nameof(placa));
        Chassi = string.IsNullOrWhiteSpace(chassi) ? null : chassi.Trim();
        KmAtual = kmAtual;
    }

    public void AtualizarKm(int km) => KmAtual = km;

    public void AtualizarDados(string marca, string modelo, int ano, Placa placa, string? chassi, int? kmAtual)
    {
        Marca = string.IsNullOrWhiteSpace(marca) ? throw new ArgumentException("Marca obrigatória") : marca.Trim();
        Modelo = string.IsNullOrWhiteSpace(modelo) ? throw new ArgumentException("Modelo obrigatório") : modelo.Trim();
        Ano = ano;
        Placa = placa ?? throw new ArgumentNullException(nameof(placa));
        Chassi = string.IsNullOrWhiteSpace(chassi) ? null : chassi.Trim();
        KmAtual = kmAtual;
    }

}
