using Oficina.SharedKernel.Domain;
using System.Collections.Generic;
namespace Oficina.Estoque.Domain;
public class Peca : Entity
{
 public string Codigo { get; set; } = default!;
 public string Descricao { get; set; } = default!;
 public decimal Preco_Unitario { get; set; }
 public int Quantidade { get; set; }
 public int Estoque_Minimo { get; set; }
 public int Estoque_Maximo { get; set; }
 public string Unidade { get; set; } = "UN";
 public string Status { get; set; } = "Ativo";
 public string? Observacoes { get; set; }
 public DateTime Data_Cadastro { get; set; } = DateTime.UtcNow;
 public long? Fabricante_Id { get; set; }
 public long? Categoria_Id { get; set; }
 public long? Localizacao_Id { get; set; }
 public ICollection<PecaFornecedor> Fornecedores { get; set; } = new List<PecaFornecedor>();
 public ICollection<PecaAnexo> Anexos { get; set; } = new List<PecaAnexo>();
 public ICollection<PecaHistorico> Historicos { get; set; } = new List<PecaHistorico>();
}


