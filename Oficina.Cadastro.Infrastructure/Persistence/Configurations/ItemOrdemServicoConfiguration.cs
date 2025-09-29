using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class ItemOrdemServicoConfiguration : IEntityTypeConfiguration<ItemOrdemServico>
{
    public void Configure(EntityTypeBuilder<ItemOrdemServico> builder)
    {
        builder.ToTable("itens_ordem_servico");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.OrdemServicoId).HasColumnType("uuid");
        builder.Property(x => x.ServicoId).HasColumnType("uuid");
        builder.Property(x => x.PecaId).HasColumnType("uuid");

        builder.Property(x => x.Tipo)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Quantidade).HasColumnType("numeric(12,3)");
        builder.Property(x => x.Preco).HasColumnType("numeric(12,2)");

        builder.HasOne(x => x.OrdemServico)
            .WithMany(o => o.Itens)
            .HasForeignKey(x => x.OrdemServicoId);

        builder.HasOne(x => x.Servico)
            .WithMany()
            .HasForeignKey(x => x.ServicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Peca)
            .WithMany()
            .HasForeignKey(x => x.PecaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}