using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain;
namespace Oficina.Cadastro.Infrastructure;
public class CadastroDbContext : DbContext
{
    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ClientePessoaFisica> ClientesPessoaFisica => Set<ClientePessoaFisica>();
    public DbSet<ClientePessoaJuridica> ClientesPessoaJuridica => Set<ClientePessoaJuridica>();
    public DbSet<ClienteEndereco> ClientesEnderecos => Set<ClienteEndereco>();
    public DbSet<ClienteContato> ClientesContatos => Set<ClienteContato>();
    public DbSet<ClienteConsentimento> ClientesConsentimentos => Set<ClienteConsentimento>();
    public DbSet<ClienteVeiculo> ClientesVeiculos => Set<ClienteVeiculo>();
    public DbSet<ClienteAnexo> ClientesAnexos => Set<ClienteAnexo>();
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(e=>{
            e.ToTable("cad_clientes");
            e.Property(p=>p.Codigo).ValueGeneratedNever();
            e.HasIndex(p=>p.Codigo).IsUnique();
            e.Property(p=>p.Nome).HasMaxLength(180).IsRequired();
            e.Property(p=>p.Observacoes).HasMaxLength(500);
            e.Property(p=>p.Tipo).HasConversion<string>().HasMaxLength(32).IsRequired();
            e.Property(p=>p.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
            e.Property(p=>p.Origem).HasConversion<string>().HasMaxLength(32).IsRequired();

            e.HasOne(p=>p.PessoaFisica)
                .WithOne()
                .HasForeignKey<ClientePessoaFisica>(pf=>pf.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(p=>p.PessoaJuridica)
                .WithOne()
                .HasForeignKey<ClientePessoaJuridica>(pj=>pj.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p=>p.Enderecos)
                .WithOne()
                .HasForeignKey(e=>e.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p=>p.Contatos)
                .WithOne()
                .HasForeignKey(c=>c.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p=>p.Consentimentos)
                .WithOne()
                .HasForeignKey(c=>c.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p=>p.Veiculos)
                .WithOne()
                .HasForeignKey(v=>v.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p=>p.Anexos)
                .WithOne()
                .HasForeignKey(a=>a.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientePessoaFisica>(e => {
            e.ToTable("cad_clientes_pf");
            e.Property(p => p.Cpf).HasMaxLength(14).IsRequired();
            e.Property(p => p.Rg).HasMaxLength(20);
            e.Property(p => p.Genero).HasMaxLength(30);
        });

        modelBuilder.Entity<ClientePessoaJuridica>(e => {
            e.ToTable("cad_clientes_pj");
            e.Property(p => p.Cnpj).HasMaxLength(18).IsRequired();
            e.Property(p => p.Razao_Social).HasMaxLength(180).IsRequired();
            e.Property(p => p.Nome_Fantasia).HasMaxLength(180);
            e.Property(p => p.Inscricao_Estadual).HasMaxLength(30);
            e.Property(p => p.Responsavel).HasMaxLength(120);
        });

        modelBuilder.Entity<ClienteEndereco>(e => {
            e.ToTable("cad_clientes_enderecos");
            e.Property(p => p.Tipo).HasConversion<string>().HasMaxLength(32).IsRequired();
            e.Property(p => p.Cep).HasMaxLength(12).IsRequired();
            e.Property(p => p.Logradouro).HasMaxLength(160).IsRequired();
            e.Property(p => p.Numero).HasMaxLength(20).IsRequired();
            e.Property(p => p.Bairro).HasMaxLength(120).IsRequired();
            e.Property(p => p.Cidade).HasMaxLength(120).IsRequired();
            e.Property(p => p.Estado).HasMaxLength(60).IsRequired();
            e.Property(p => p.Pais).HasMaxLength(60).IsRequired();
            e.Property(p => p.Complemento).HasMaxLength(160);
        });

        modelBuilder.Entity<ClienteContato>(e => {
            e.ToTable("cad_clientes_contatos");
            e.Property(p => p.Tipo).HasConversion<string>().HasMaxLength(32).IsRequired();
            e.Property(p => p.Valor).HasMaxLength(120).IsRequired();
            e.Property(p => p.Observacao).HasMaxLength(200);
        });

        modelBuilder.Entity<ClienteConsentimento>(e => {
            e.ToTable("cad_clientes_consentimentos");
            e.Property(p => p.Tipo).HasConversion<string>().HasMaxLength(40).IsRequired();
            e.Property(p => p.Observacoes).HasMaxLength(200);
        });

        modelBuilder.Entity<ClienteVeiculo>(e => {
            e.ToTable("cad_clientes_veiculos");
            e.Property(p => p.Placa).HasMaxLength(10).IsRequired();
            e.Property(p => p.Marca).HasMaxLength(80);
            e.Property(p => p.Modelo).HasMaxLength(120);
            e.Property(p => p.Cor).HasMaxLength(60);
            e.Property(p => p.Chassi).HasMaxLength(30);
        });

        modelBuilder.Entity<ClienteAnexo>(e => {
            e.ToTable("cad_clientes_anexos");
            e.Property(p => p.Nome).HasMaxLength(160).IsRequired();
            e.Property(p => p.Tipo).HasMaxLength(80).IsRequired();
            e.Property(p => p.Url).HasMaxLength(300).IsRequired();
            e.Property(p => p.Observacao).HasMaxLength(200);
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
