using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain;

namespace Oficina.Cadastro.Infrastructure;

public class CadastroDbContext : DbContext
{
    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ClienteOrigem> ClienteOrigens => Set<ClienteOrigem>();
    public DbSet<PessoaPf> PessoasPf => Set<PessoaPf>();
    public DbSet<PessoaPj> PessoasPj => Set<PessoaPj>();
    public DbSet<ClienteEndereco> ClienteEnderecos => Set<ClienteEndereco>();
    public DbSet<ClienteContato> ClienteContatos => Set<ClienteContato>();
    public DbSet<ClienteIndicacao> ClienteIndicacoes => Set<ClienteIndicacao>();
    public DbSet<ClienteLgpdConsentimento> ClienteLgpdConsentimentos => Set<ClienteLgpdConsentimento>();
    public DbSet<ClienteFinanceiro> ClienteFinanceiro => Set<ClienteFinanceiro>();
    public DbSet<VeiculoMarca> VeiculoMarcas => Set<VeiculoMarca>();
    public DbSet<VeiculoModelo> VeiculoModelos => Set<VeiculoModelo>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<ClienteAnexo> ClienteAnexos => Set<ClienteAnexo>();
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cad_clientes");
            entity.Property(p => p.Nome).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Documento).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Telefone).HasMaxLength(20);
            entity.Property(p => p.Email).HasMaxLength(160);
            entity.HasIndex(p => p.Documento).IsUnique();
            entity.HasOne(p => p.Origem)
                .WithMany(o => o.Clientes)
                .HasForeignKey(p => p.Origem_Id)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(p => p.Enderecos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Contatos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Indicacoes)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Veiculos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Anexos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClienteOrigem>(entity =>
        {
            entity.ToTable("cad_clientes_origens");
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Descricao).HasMaxLength(240);
            entity.HasIndex(p => p.Nome).IsUnique();
        });

        modelBuilder.Entity<PessoaPf>(entity =>
        {
            entity.ToTable("cad_clientes_pf");
            entity.Property(p => p.Cpf).HasMaxLength(14).IsRequired();
            entity.Property(p => p.Rg).HasMaxLength(20);
            entity.Property(p => p.Genero).HasMaxLength(20);
            entity.Property(p => p.Estado_Civil).HasMaxLength(20);
            entity.Property(p => p.Profissao).HasMaxLength(120);
            entity.HasIndex(p => p.Cpf).IsUnique();
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithOne(c => c.PessoaPf)
                .HasForeignKey<PessoaPf>(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PessoaPj>(entity =>
        {
            entity.ToTable("cad_clientes_pj");
            entity.Property(p => p.Razao_Social).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Nome_Fantasia).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Cnpj).HasMaxLength(18).IsRequired();
            entity.Property(p => p.Inscricao_Estadual).HasMaxLength(30);
            entity.Property(p => p.Inscricao_Municipal).HasMaxLength(30);
            entity.HasIndex(p => p.Cnpj).IsUnique();
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithOne(c => c.PessoaPj)
                .HasForeignKey<PessoaPj>(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClienteEndereco>(entity =>
        {
            entity.ToTable("cad_clientes_enderecos");
            entity.Property(p => p.Tipo).HasMaxLength(40).IsRequired();
            entity.Property(p => p.Logradouro).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Numero).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Complemento).HasMaxLength(120);
            entity.Property(p => p.Bairro).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Cidade).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Estado).HasMaxLength(60).IsRequired();
            entity.Property(p => p.Cep).HasMaxLength(12).IsRequired();
            entity.Property(p => p.Pais).HasMaxLength(80);
            entity.Property(p => p.Principal).HasDefaultValue(false);
            entity.HasIndex(p => new { p.Cliente_Id, p.Tipo, p.Principal });
        });

        modelBuilder.Entity<ClienteContato>(entity =>
        {
            entity.ToTable("cad_clientes_contatos");
            entity.Property(p => p.Tipo).HasMaxLength(40).IsRequired();
            entity.Property(p => p.Valor).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.Property(p => p.Principal).HasDefaultValue(false);
            entity.HasIndex(p => new { p.Cliente_Id, p.Tipo, p.Valor }).IsUnique();
        });

        modelBuilder.Entity<ClienteIndicacao>(entity =>
        {
            entity.ToTable("cad_clientes_indicacoes");
            entity.Property(p => p.Indicador_Nome).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Indicador_Telefone).HasMaxLength(20);
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => p.Cliente_Id);
        });

        modelBuilder.Entity<ClienteLgpdConsentimento>(entity =>
        {
            entity.ToTable("cad_clientes_lgpd_consentimentos");
            entity.Property(p => p.Canal).HasMaxLength(80).IsRequired();
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithOne(c => c.LgpdConsentimento)
                .HasForeignKey<ClienteLgpdConsentimento>(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClienteFinanceiro>(entity =>
        {
            entity.ToTable("cad_clientes_financeiro");
            entity.Property(p => p.Limite_Credito).HasColumnType("decimal(10,2)");
            entity.Property(p => p.Observacoes).HasMaxLength(500);
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithOne(c => c.Financeiro)
                .HasForeignKey<ClienteFinanceiro>(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VeiculoMarca>(entity =>
        {
            entity.ToTable("cad_veiculos_marcas");
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Pais).HasMaxLength(80);
            entity.HasIndex(p => p.Nome).IsUnique();
        });

        modelBuilder.Entity<VeiculoModelo>(entity =>
        {
            entity.ToTable("cad_veiculos_modelos");
            entity.Property(p => p.Nome).HasMaxLength(160).IsRequired();
            entity.HasIndex(p => new { p.Marca_Id, p.Nome }).IsUnique();
            entity.HasOne(p => p.Marca)
                .WithMany(m => m.Modelos)
                .HasForeignKey(p => p.Marca_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.ToTable("cad_veiculos");
            entity.Property(p => p.Placa).HasMaxLength(10).IsRequired();
            entity.Property(p => p.Cor).HasMaxLength(40);
            entity.Property(p => p.Renavam).HasMaxLength(20);
            entity.Property(p => p.Chassi).HasMaxLength(40);
            entity.Property(p => p.Combustivel).HasMaxLength(40);
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => p.Placa).IsUnique();
            entity.HasIndex(p => p.Renavam)
                .IsUnique()
                .HasFilter("[Renavam] IS NOT NULL");
            entity.HasOne(p => p.Modelo)
                .WithMany(m => m.Veiculos)
                .HasForeignKey(p => p.Modelo_Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClienteAnexo>(entity =>
        {
            entity.ToTable("cad_clientes_anexos");
            entity.Property(p => p.Nome_Arquivo).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Tipo_Conteudo).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Caminho_Arquivo).HasMaxLength(500).IsRequired();
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => new { p.Cliente_Id, p.Nome_Arquivo }).IsUnique();
        });

        modelBuilder.Entity<Mecanico>(entity =>
        {
            entity.ToTable("cad_mecanicos");
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.ToTable("cad_fornecedores");
            entity.Property(p => p.Razao_Social).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Cnpj).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Contato).HasMaxLength(160).IsRequired();
            entity.HasIndex(p => p.Cnpj).IsUnique();
        });
    }
}
