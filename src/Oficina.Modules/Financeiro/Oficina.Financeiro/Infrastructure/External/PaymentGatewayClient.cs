namespace Oficina.Financeiro.Infrastructure.External;
public interface IPaymentGatewayClient
{
    Task<(bool success, string transactionId)> ChargeAsync(string meio, decimal valor);
}
public class FakePaymentGatewayClient : IPaymentGatewayClient
{
    public Task<(bool success, string transactionId)> ChargeAsync(string meio, decimal valor)
    {
        var ok = valor >= 1.00m;
        var id = $"TX-{DateTime.UtcNow.Ticks}";
        return Task.FromResult((ok, id));
    }
}
