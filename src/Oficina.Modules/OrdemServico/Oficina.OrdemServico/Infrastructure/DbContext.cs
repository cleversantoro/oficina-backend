using Microsoft.EntityFrameworkCore;
using Oficina.OrdemServico.Domain;
namespace Oficina.OrdemServico.Infrastructure;
public class OrdemServicoDbContext : DbContext
{
    public OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : base(options) { }
    public DbSet<Oficina.OrdemServico.Domain.OrdemServico> Ordens => Set<Oficina.OrdemServico.Domain.OrdemServico>();
    public DbSet<ItemServico> Itens => Set<ItemServico>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Oficina.OrdemServico.Domain.OrdemServico>(e=>{
            e.ToTable("os_ordens");
            e.Property(p=>p.Status).HasMaxLength(20).IsRequired();
            e.Property(p=>p.Descricao_Problema).HasMaxLength(500).IsRequired();
            e.Property(p=>p.Data_Abertura).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Data_Conclusao);
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
            e.HasMany(p=>p.Itens).WithOne().HasForeignKey(i=>i.Ordem_Servico_Id);
            e.HasMany(p=>p.Anexos).WithOne().HasForeignKey(a=>a.Ordem_Servico_Id);
            e.HasMany(p=>p.Historicos).WithOne().HasForeignKey(h=>h.Ordem_Servico_Id);
            e.HasMany(p=>p.Checklists).WithOne().HasForeignKey(c=>c.Ordem_Servico_Id);
            e.HasMany(p=>p.Avaliacoes).WithOne().HasForeignKey(a=>a.Ordem_Servico_Id);
            e.HasMany(p=>p.Pagamentos).WithOne().HasForeignKey(p=>p.Ordem_Servico_Id);
            e.HasMany(p=>p.Observacoes).WithOne().HasForeignKey(o=>o.Ordem_Servico_Id);
        });
        modelBuilder.Entity<ItemServico>(e=>{
            e.ToTable("os_itens");
            e.Property(p=>p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p=>p.Valor_Unitario).HasColumnType("decimal(12,2)");
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoAnexo>(e=>{
            e.ToTable("os_anexos");
            e.Property(p=>p.Nome).HasMaxLength(200);
            e.Property(p=>p.Tipo).HasMaxLength(50);
            e.Property(p=>p.Url).HasMaxLength(500);
            e.Property(p=>p.Observacao).HasMaxLength(200);
            e.Property(p=>p.Data_Upload).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoHistorico>(e=>{
            e.ToTable("os_ordens_historico");
            e.Property(p=>p.Data_Alteracao).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Usuario).HasMaxLength(100);
            e.Property(p=>p.Campo).HasMaxLength(100);
            e.Property(p=>p.Valor_Antigo).HasColumnType("text");
            e.Property(p=>p.Valor_Novo).HasColumnType("text");
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoChecklist>(e=>{
            e.ToTable("os_checklists");
            e.Property(p=>p.Item).HasMaxLength(200).IsRequired();
            e.Property(p=>p.Observacao).HasMaxLength(200);
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoAvaliacao>(e=>{
            e.ToTable("os_avaliacoes");
            e.Property(p=>p.Nota).IsRequired();
            e.Property(p=>p.Comentario).HasMaxLength(500);
            e.Property(p=>p.Usuario).HasMaxLength(100);
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoPagamento>(e=>{
            e.ToTable("os_pagamentos");
            e.Property(p=>p.Valor).HasColumnType("decimal(12,2)");
            e.Property(p=>p.Status).HasMaxLength(20).IsRequired();
            e.Property(p=>p.Metodo).HasMaxLength(50);
            e.Property(p=>p.Observacao).HasMaxLength(200);
            e.Property(p=>p.Data_Pagamento);
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<OrdemServicoObservacao>(e=>{
            e.ToTable("os_observacoes");
            e.Property(p=>p.Usuario).HasMaxLength(100);
            e.Property(p=>p.Texto).HasColumnType("text").IsRequired();
            e.Property(p=>p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p=>p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
    }
}


