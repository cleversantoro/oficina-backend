using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain;
namespace Oficina.Cadastro.Infrastructure;
public class CadastroDbContext : DbContext
{
    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e=>{
            e.ToTable("cad_clientes");
            e.Property(p=>p.Nome).HasMaxLength(120).IsRequired();
            e.Property(p=>p.Documento).HasMaxLength(20).IsRequired();
            e.HasIndex(p=>p.Documento).IsUnique();
        });
        modelBuilder.Entity<Mecanico>(e=>{
            e.ToTable("cad_mecanicos");
            e.Property(p=>p.Nome).HasMaxLength(120).IsRequired();
        });
        modelBuilder.Entity<Fornecedor>(e=>{
            e.ToTable("cad_fornecedores");
            e.Property(p=>p.Razao_Social).HasMaxLength(160).IsRequired();
            e.Property(p=>p.Cnpj).HasMaxLength(20).IsRequired();
            e.HasIndex(p=>p.Cnpj).IsUnique();
        });
    }
}
