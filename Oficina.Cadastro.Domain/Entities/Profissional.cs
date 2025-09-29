namespace Oficina.Cadastro.Domain.Entities
{
    public class Profissional
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Especialidade { get; set; }
    }
}