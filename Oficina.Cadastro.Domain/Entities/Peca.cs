namespace Oficina.Cadastro.Domain.Entities
{
    public class Peca
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}