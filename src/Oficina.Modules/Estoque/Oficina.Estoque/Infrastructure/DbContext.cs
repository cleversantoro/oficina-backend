using Microsoft.EntityFrameworkCore;
using Oficina.Estoque.Domain;
namespace Oficina.Estoque.Infrastructure;
public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options) { }
    public DbSet<Peca> Pecas => Set<Peca>();
    public DbSet<Movimentacao> Movimentacoes => Set<Movimentacao>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Peca>(e=>{
            e.ToTable("est_pecas");
            e.Property(p=>p.Codigo).HasMaxLength(40).IsRequired();
            e.Property(p=>p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p=>p.PrecoUnitario).HasColumnType("decimal(18,2)");
            e.HasIndex(p=>p.Codigo).IsUnique();
        });
        modelBuilder.Entity<Movimentacao>(e=>{
            e.ToTable("est_movimentacoes");
            e.Property(p=>p.Tipo).HasMaxLength(10).IsRequired();
        });
    }
}
