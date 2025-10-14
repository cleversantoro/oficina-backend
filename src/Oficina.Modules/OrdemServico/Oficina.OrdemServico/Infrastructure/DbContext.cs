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
            e.HasMany(p=>p.Itens).WithOne().HasForeignKey(i=>i.Ordem_Servico_Id);
        });
        modelBuilder.Entity<ItemServico>(e=>{
            e.ToTable("os_itens");
            e.Property(p=>p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p=>p.Valor_Unitario).HasColumnType("decimal(18,2)");
        });
    }
}

