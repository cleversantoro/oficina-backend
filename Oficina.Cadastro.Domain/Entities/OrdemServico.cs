using System;
using System.Collections.Generic;

namespace Oficina.Cadastro.Domain.Entities
{
    public class OrdemServico
    {
        public Guid Id { get; set; }
        public Guid MotoId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ProfissionalId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal Total { get; set; }

        public Moto? Moto { get; set; }
        public Cliente? Cliente { get; set; }
        public Profissional? Profissional { get; set; }
        public ICollection<ItemOrdemServico> Itens { get; set; } = new List<ItemOrdemServico>();
    }
}