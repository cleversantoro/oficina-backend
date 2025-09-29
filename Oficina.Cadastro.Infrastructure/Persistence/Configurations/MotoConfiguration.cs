using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;
using Oficina.SharedKernel.ValueObjects;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class MotoConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> builder)
    {
        builder.ToTable("motos", CadastroDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.ClienteId)
            .HasColumnType("uuid");

        builder.Property(x => x.Marca)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.Modelo)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.Ano)
            .IsRequired();

        builder.Property(x => x.Placa)
            .HasConversion(placa => placa.Value, value => new Placa(value))
            .HasColumnName("placa")
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(x => x.Chassi)
            .HasMaxLength(30);

        builder.Property(x => x.KmAtual);

        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Placa)
            .IsUnique();
    }
}
