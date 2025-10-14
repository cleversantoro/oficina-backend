using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class MecanicoDisponibilidade : Entity
{
    public long Mecanico_Id { get; set; }
    public Mecanico Mecanico { get; set; } = default!;
    public byte Dia_Semana { get; set; }
    public TimeSpan Hora_Inicio { get; set; }
    public TimeSpan Hora_Fim { get; set; }
    public int Capacidade_Atendimentos { get; set; } = 5;
}
