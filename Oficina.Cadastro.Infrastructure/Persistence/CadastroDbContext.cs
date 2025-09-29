using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence.Configurations;

namespace Oficina.Cadastro.Infrastructure.Persistence;

public class CadastroDbContext : DbContext
{
    public const string Schema = "oficina";

    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }

    public DbSet<Moto> Motos => Set<Moto>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Profissional> Profissionais => Set<Profissional>();
    public DbSet<Servico> Servicos => Set<Servico>();
    public DbSet<Peca> Pecas => Set<Peca>();
    public DbSet<OrdemServico> OrdensServico => Set<OrdemServico>();
    public DbSet<ItemOrdemServico> ItensOrdemServico => Set<ItemOrdemServico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.ApplyConfiguration(new MotoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ProfissionalConfiguration());
        modelBuilder.ApplyConfiguration(new ServicoConfiguration());
        modelBuilder.ApplyConfiguration(new PecaConfiguration());
        modelBuilder.ApplyConfiguration(new OrdemServicoConfiguration());
        modelBuilder.ApplyConfiguration(new ItemOrdemServicoConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
