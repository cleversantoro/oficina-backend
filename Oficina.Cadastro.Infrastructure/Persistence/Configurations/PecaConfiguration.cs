using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class PecaConfiguration : IEntityTypeConfiguration<Peca>
{
    public void Configure(EntityTypeBuilder<Peca> builder)
    {
        builder.ToTable("pecas", CadastroDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Codigo)
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Preco)
            .HasColumnType("numeric(12,2)");

        builder.HasIndex(x => x.Codigo)
            .IsUnique();
    }
}
