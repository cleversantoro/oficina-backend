namespace Oficina.Cadastro.Domain.Entities
{
    public class Servico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
    }
}