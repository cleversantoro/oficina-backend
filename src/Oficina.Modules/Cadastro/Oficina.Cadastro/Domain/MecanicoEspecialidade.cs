using System.Collections.Generic;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoEspecialidade : Entity
{
    public string Codigo { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public ICollection<MecanicoEspecialidadeRel> Mecanicos { get; set; } = new List<MecanicoEspecialidadeRel>();
    public ICollection<MecanicoCertificacao> Certificacoes { get; set; } = new List<MecanicoCertificacao>();
}
