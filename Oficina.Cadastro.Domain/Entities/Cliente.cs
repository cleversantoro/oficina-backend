using Oficina.SharedKernel.ValueObjects;

namespace Oficina.Cadastro.Domain.Entities;
public class Cliente
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; }
    public string Documento { get; private set; } // CPF/CNPJ (sem máscara)
    public Email Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public string? Endereco { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private Cliente() { } // EF

    public Cliente(string nome, string documento, Email email, Telefone telefone, string? endereco)
    {
        SetNome(nome);
        Documento = documento?.Trim() ?? throw new ArgumentNullException(nameof(documento));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
        Endereco = endereco;
    }

    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório");
        Nome = nome.Trim();
    }

    public void AlterarContato(Email email, Telefone telefone)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
    }

    public void AlterarEndereco(string? endereco) => Endereco = endereco;
}
