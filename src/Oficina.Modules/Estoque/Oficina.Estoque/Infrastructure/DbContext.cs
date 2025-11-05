using Microsoft.EntityFrameworkCore;
using Oficina.Estoque.Domain;
namespace Oficina.Estoque.Infrastructure;
public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options) { }
    public DbSet<Peca> Pecas => Set<Peca>();
    public DbSet<Movimentacao> Movimentacoes => Set<Movimentacao>();
    public DbSet<Fabricante> Fabricantes => Set<Fabricante>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
    public DbSet<PecaFornecedor> PecasFornecedores => Set<PecaFornecedor>();
    public DbSet<PecaAnexo> PecasAnexos => Set<PecaAnexo>();
    public DbSet<PecaHistorico> PecasHistoricos => Set<PecaHistorico>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fabricante>(e => {
            e.ToTable("est_fabricantes");
            e.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            e.Property(p => p.Cnpj).HasMaxLength(20);
            e.Property(p => p.Contato).HasMaxLength(100);
            e.HasIndex(p => p.Nome).IsUnique();
        });
        modelBuilder.Entity<Categoria>(e => {
            e.ToTable("est_categorias");
            e.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            e.Property(p => p.Descricao).HasMaxLength(200);
            e.HasIndex(p => p.Nome).IsUnique();
        });
        modelBuilder.Entity<Localizacao>(e => {
            e.ToTable("est_localizacoes");
            e.Property(p => p.Descricao).HasMaxLength(100).IsRequired();
            e.Property(p => p.Corredor).HasMaxLength(20);
            e.Property(p => p.Prateleira).HasMaxLength(20);
        });
        modelBuilder.Entity<Peca>(e => {
            e.ToTable("est_pecas");
            e.Property(p => p.Codigo).HasMaxLength(50).IsRequired();
            e.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
            e.Property(p => p.Preco_Unitario).HasColumnType("decimal(12,2)");
            e.Property(p => p.Unidade).HasMaxLength(10).HasDefaultValue("UN");
            e.Property(p => p.Status).HasMaxLength(20).HasDefaultValue("Ativo");
            e.Property(p => p.Observacoes).HasMaxLength(500);
            e.Property(p => p.Data_Cadastro).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasIndex(p => p.Codigo).IsUnique();
            e.HasOne<Fabricante>().WithMany().HasForeignKey(p => p.Fabricante_Id).OnDelete(DeleteBehavior.SetNull);
            e.HasOne<Categoria>().WithMany().HasForeignKey(p => p.Categoria_Id).OnDelete(DeleteBehavior.SetNull);
            e.HasOne<Localizacao>().WithMany().HasForeignKey(p => p.Localizacao_Id).OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<PecaFornecedor>(e => {
            e.ToTable("est_pecas_fornecedores");
            e.Property(p => p.Preco).HasColumnType("decimal(12,2)");
            e.Property(p => p.Prazo_Entrega);
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.HasIndex(p => new { p.Peca_Id, p.Fornecedor_Id }).IsUnique();
        });
        modelBuilder.Entity<Movimentacao>(e => {
            e.ToTable("est_movimentacoes");
            e.Property(p => p.Tipo).HasMaxLength(10).IsRequired();
            e.Property(p => p.Data_Movimentacao).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(p => p.Usuario).HasMaxLength(100);
        });
        modelBuilder.Entity<PecaAnexo>(e => {
            e.ToTable("est_pecas_anexos");
            e.Property(p => p.Nome).HasMaxLength(200);
            e.Property(p => p.Tipo).HasMaxLength(50);
            e.Property(p => p.Url).HasMaxLength(500);
            e.Property(p => p.Observacao).HasMaxLength(200);
            e.Property(p => p.Data_Upload).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<PecaHistorico>(e => {
            e.ToTable("est_pecas_historico");
            e.Property(p => p.Data_Alteracao).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(p => p.Usuario).HasMaxLength(100);
            e.Property(p => p.Campo).HasMaxLength(100);
            e.Property(p => p.Valor_Antigo).HasColumnType("text");
            e.Property(p => p.Valor_Novo).HasColumnType("text");
        });
    }
}


