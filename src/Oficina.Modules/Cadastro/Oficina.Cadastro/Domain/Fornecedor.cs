using Oficina.SharedKernel.Domain;
using System.Collections.Generic;

namespace Oficina.Cadastro.Domain;

public class Fornecedor : Entity
{
    public string Razao_Social { get; set; } = default!;
    public string? Nome_Fantasia { get; set; }
    public string Cnpj { get; set; } = default!;
    public string? Inscricao_Estadual { get; set; }
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Observacoes { get; set; }
    public string Status { get; set; } = "Ativo";
    public ICollection<FornecedorEndereco> Enderecos { get; set; } = new List<FornecedorEndereco>();
    public ICollection<FornecedorContato> Contatos { get; set; } = new List<FornecedorContato>();
    public ICollection<FornecedorAnexo> Anexos { get; set; } = new List<FornecedorAnexo>();
    public ICollection<FornecedorBanco> Bancos { get; set; } = new List<FornecedorBanco>();
    public ICollection<FornecedorHistorico> Historicos { get; set; } = new List<FornecedorHistorico>();
}


