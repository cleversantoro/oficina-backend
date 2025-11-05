using Microsoft.EntityFrameworkCore;
using Oficina.Financeiro.Domain;
namespace Oficina.Financeiro.Infrastructure;
public class FinanceiroDbContext : DbContext
{
    public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options) : base(options) { }
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<NFe> NFes => Set<NFe>();
    public DbSet<MetodoPagamento> MetodosPagamento => Set<MetodoPagamento>();
    public DbSet<ContaPagar> ContasPagar => Set<ContaPagar>();
    public DbSet<ContaReceber> ContasReceber => Set<ContaReceber>();
    public DbSet<Lancamento> Lancamentos => Set<Lancamento>();
    public DbSet<FinanceiroAnexo> Anexos => Set<FinanceiroAnexo>();
    public DbSet<FinanceiroHistorico> Historicos => Set<FinanceiroHistorico>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamento>(e=>{
            e.ToTable("fin_pagamentos");
            e.Property(p=>p.Meio).HasMaxLength(50);
            e.Property(p=>p.Status).HasMaxLength(20).IsRequired();
            e.Property(p=>p.Valor).HasColumnType("decimal(12,2)");
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Pagamento);
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<NFe>(e=>{
            e.ToTable("fin_nfes");
            e.Property(p=>p.Numero).HasMaxLength(20).IsRequired();
            e.Property(p=>p.Chave_Acesso).HasMaxLength(60).IsRequired();
        });
        modelBuilder.Entity<MetodoPagamento>(e => {
            e.ToTable("fin_metodos_pagamento");
            e.Property(p => p.Nome).HasMaxLength(50).IsRequired();
            e.Property(p => p.Descricao).HasMaxLength(200);
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<ContaPagar>(e => {
            e.ToTable("fin_contas_pagar");
            e.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p => p.Valor).HasColumnType("decimal(12,2)");
            e.Property(p => p.Status).HasMaxLength(20).IsRequired();
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Pagamento);
            e.Property(p => p.Vencimento);
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<ContaReceber>(e => {
            e.ToTable("fin_contas_receber");
            e.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p => p.Valor).HasColumnType("decimal(12,2)");
            e.Property(p => p.Status).HasMaxLength(20).IsRequired();
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Recebimento);
            e.Property(p => p.Vencimento);
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<Lancamento>(e => {
            e.ToTable("fin_lancamentos");
            e.Property(p => p.Tipo).HasMaxLength(10).IsRequired();
            e.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p => p.Valor).HasColumnType("decimal(12,2)");
            e.Property(p => p.Referencia).HasMaxLength(100);
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Lancamento).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<FinanceiroAnexo>(e => {
            e.ToTable("fin_anexos");
            e.Property(p => p.Nome).HasMaxLength(200);
            e.Property(p => p.Tipo).HasMaxLength(50);
            e.Property(p => p.Url).HasMaxLength(500);
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Upload).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
        modelBuilder.Entity<FinanceiroHistorico>(e => {
            e.ToTable("fin_historico");
            e.Property(p => p.Entidade).HasMaxLength(50).IsRequired();
            e.Property(p => p.Campo).HasMaxLength(100);
            e.Property(p => p.Usuario).HasMaxLength(100);
            e.Property(p => p.Valor_Antigo).HasColumnType("text");
            e.Property(p => p.Valor_Novo).HasColumnType("text");
            e.Property(p => p.Data_Alteracao).HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Created_At).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(3)");
            e.Property(p => p.Updated_At).HasColumnName("updated_at").HasDefaultValueSql(null).ValueGeneratedOnUpdate();
        });
    }
}


