using System;
using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class Mecanico : Entity
{
    public string Codigo { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string? Sobrenome { get; set; }
    public string? Nome_Social { get; set; }
    public string Documento_Principal { get; set; } = default!;
    public int Tipo_Documento { get; set; } = 1;
    public DateTime? Data_Nascimento { get; set; }
    public DateTime Data_Admissao { get; set; }
    public DateTime? Data_Demissao { get; set; }
    public string Status { get; set; } = "Ativo";
    public long? Especialidade_Principal_Id { get; set; }
    public MecanicoEspecialidade? EspecialidadePrincipal { get; set; }
    public string Nivel { get; set; } = "Pleno";
    public decimal Valor_Hora { get; set; }
    public int Carga_Horaria_Semanal { get; set; } = 44;
    public string? Observacoes { get; set; }
    public ICollection<MecanicoEspecialidadeRel> Especialidades { get; set; } = new List<MecanicoEspecialidadeRel>();
    public ICollection<MecanicoDocumento> Documentos { get; set; } = new List<MecanicoDocumento>();
    public ICollection<MecanicoContato> Contatos { get; set; } = new List<MecanicoContato>();
    public ICollection<MecanicoEndereco> Enderecos { get; set; } = new List<MecanicoEndereco>();
    public ICollection<MecanicoCertificacao> Certificacoes { get; set; } = new List<MecanicoCertificacao>();
    public ICollection<MecanicoDisponibilidade> Disponibilidades { get; set; } = new List<MecanicoDisponibilidade>();
    public ICollection<MecanicoExperiencia> Experiencias { get; set; } = new List<MecanicoExperiencia>();
}
