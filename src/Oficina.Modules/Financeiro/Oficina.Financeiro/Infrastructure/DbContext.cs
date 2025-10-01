using Microsoft.EntityFrameworkCore;
using Oficina.Financeiro.Domain;
namespace Oficina.Financeiro.Infrastructure;
public class FinanceiroDbContext : DbContext
{
    public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options) : base(options) { }
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<NFe> NFes => Set<NFe>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamento>(e=>{
            e.ToTable("fin_pagamentos");
            e.Property(p=>p.Meio).HasMaxLength(12).IsRequired();
            e.Property(p=>p.Status).HasMaxLength(12).IsRequired();
            e.Property(p=>p.Valor).HasColumnType("decimal(18,2)");
        });
        modelBuilder.Entity<NFe>(e=>{
            e.ToTable("fin_nfes");
            e.Property(p=>p.Numero).HasMaxLength(20).IsRequired();
            e.Property(p=>p.ChaveAcesso).HasMaxLength(60).IsRequired();
        });
    }
}
