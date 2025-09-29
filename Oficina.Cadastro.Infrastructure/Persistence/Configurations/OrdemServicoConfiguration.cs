using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class OrdemServicoConfiguration : IEntityTypeConfiguration<OrdemServico>
{
    public void Configure(EntityTypeBuilder<OrdemServico> builder)
    {
        builder.ToTable("ordens_servico", CadastroDbContext.Schema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.MotoId).HasColumnType("uuid");
        builder.Property(x => x.ClienteId).HasColumnType("uuid");
        builder.Property(x => x.ProfissionalId).HasColumnType("uuid");

        builder.Property(x => x.Status)
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(x => x.DataAbertura);
        builder.Property(x => x.DataFechamento);
        builder.Property(x => x.Total).HasColumnType("numeric(12,2)");

        builder.HasOne(x => x.Moto).WithMany().HasForeignKey(x => x.MotoId);
        builder.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId);
        builder.HasOne(x => x.Profissional).WithMany().HasForeignKey(x => x.ProfissionalId);

        builder.HasIndex(x => new { x.DataAbertura, x.Status });
    }
}