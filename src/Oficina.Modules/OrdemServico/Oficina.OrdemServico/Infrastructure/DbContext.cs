using Microsoft.EntityFrameworkCore;
using Oficina.OrdemServico.Domain;
namespace Oficina.OrdemServico.Infrastructure;
public class OrdemServicoDbContext : DbContext
{
    public OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : base(options) { }
    public DbSet<OrdemServico> Ordens => Set<OrdemServico>();
    public DbSet<ItemServico> Itens => Set<ItemServico>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdemServico>(e=>{
            e.ToTable("os_ordens");
            e.Property(p=>p.Status).HasMaxLength(20).IsRequired();
            e.HasMany(p=>p.Itens).WithOne().HasForeignKey(i=>i.OrdemServicoId);
        });
        modelBuilder.Entity<ItemServico>(e=>{
            e.ToTable("os_itens");
            e.Property(p=>p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p=>p.ValorUnitario).HasColumnType("decimal(18,2)");
        });
    }
}
