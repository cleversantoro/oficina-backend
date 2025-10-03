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

            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
                .HasColumnName("id");

            e.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            e.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(120)
                .IsRequired();

            e.Property(p => p.NomeExibicao)
                .HasColumnName("nome_exibicao")
                .HasMaxLength(160)
                .IsRequired();

            e.Property(p => p.Documento)
                .HasColumnName("documento")
                .HasMaxLength(20)
                .IsRequired();

            e.Property(p => p.PessoaTipo)
                .HasColumnName("pessoa_tipo")
                .HasConversion<short>()
                .IsRequired();

            e.Property(p => p.Status)
                .HasColumnName("status")
                .HasConversion<short>()
                .HasDefaultValue(ClienteStatus.Ativo)
                .IsRequired();

            e.Property(p => p.ClienteVip)
                .HasColumnName("cliente_vip")
                .HasDefaultValue(false)
                .IsRequired();

            e.Property(p => p.OrigemCadastroId)
                .HasColumnName("origem_cadastro_id")
                .IsRequired();

            e.Property(p => p.Telefone)
                .HasColumnName("telefone")
                .HasMaxLength(20)
                .IsRequired();

            e.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(160)
                .IsRequired();

            e.Property(p => p.Created_At)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            e.Property(p => p.Updated_At)
                .HasColumnName("updated_at")
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                .IsRequired(false);

            e.Property(p => p.DeletedAt)
                .HasColumnName("deleted_at");

            e.HasIndex(p => p.Documento)
                .HasDatabaseName("ux_cad_clientes_documento")
                .IsUnique();

            e.HasIndex(p => p.Status)
                .HasDatabaseName("ix_cad_clientes_status");

            e.HasIndex(p => p.OrigemCadastroId)
                .HasDatabaseName("ix_cad_clientes_origem");
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
