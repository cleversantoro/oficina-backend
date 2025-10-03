namespace Oficina.Financeiro.Infrastructure.External;
public interface INfeClient
{
    Task<(bool success, string numero, string chave)> EmitirAsync(long ordemServicoId, decimal valor);
}
public class FakeNfeClient : INfeClient
{
    public Task<(bool success, string numero, string chave)> EmitirAsync(long ordemServicoId, decimal valor)
    {
        var numero = $"NFE{DateTime.UtcNow:yyyyMMddHHmmss}";
        var chave = $"CH{Guid.NewGuid():N}".ToUpper();
        return Task.FromResult((true, numero, chave));
    }
}
