using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;
using Oficina.SharedKernel.ValueObjects;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes", CadastroDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(160);

        builder.Property(x => x.Documento)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Endereco)
            .HasMaxLength(300);

        builder.Property(x => x.CriadoEm)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(email => email.Value, value => new Email(value))
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Telefone)
            .HasConversion(telefone => telefone.Value, value => new Telefone(value))
            .HasColumnName("telefone")
            .IsRequired()
            .HasMaxLength(12);

        builder.HasIndex(x => x.Documento)
            .IsUnique();
    }
}
