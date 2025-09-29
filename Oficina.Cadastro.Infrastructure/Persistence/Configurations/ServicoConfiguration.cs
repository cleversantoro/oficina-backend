using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.ToTable("servicos", CadastroDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Nome)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.PrecoBase)
            .HasColumnType("numeric(12,2)");
    }
}
