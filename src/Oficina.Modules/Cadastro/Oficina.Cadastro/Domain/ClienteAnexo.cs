using System;
using Oficina.SharedKernel.Domain;

namespace Oficina.Cadastro.Domain;

public class ClienteAnexo : Entity
{
    public Guid Cliente_Id { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public string Nome_Arquivo { get; set; } = default!;
    public string Tipo_Conteudo { get; set; } = default!;
    public string Caminho_Arquivo { get; set; } = default!;
    public string? Observacao { get; set; }
}
