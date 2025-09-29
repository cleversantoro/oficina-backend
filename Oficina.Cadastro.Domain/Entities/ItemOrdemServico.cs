using System;

namespace Oficina.Cadastro.Domain.Entities
{
    public class ItemOrdemServico
    {
        public Guid Id { get; set; }
        public Guid OrdemServicoId { get; set; }
        public Guid? ServicoId { get; set; }
        public Guid? PecaId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }

        public OrdemServico? OrdemServico { get; set; }
        public Servico? Servico { get; set; }
        public Peca? Peca { get; set; }
    }
}