using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain;

namespace Oficina.Cadastro.Infrastructure;

public class CadastroDbContext : DbContext
{
    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }

    #region Cliente
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ClienteOrigem> ClienteOrigens => Set<ClienteOrigem>();
    public DbSet<ClienteEndereco> ClienteEnderecos => Set<ClienteEndereco>();
    public DbSet<ClienteContato> ClienteContatos => Set<ClienteContato>();
    public DbSet<ClienteIndicacao> ClienteIndicacoes => Set<ClienteIndicacao>();
    public DbSet<ClienteConsentimento> ClienteConsentimentos => Set<ClienteConsentimento>();
    public DbSet<ClienteFinanceiro> ClienteFinanceiro => Set<ClienteFinanceiro>();
    public DbSet<ClienteAnexo> ClienteAnexos => Set<ClienteAnexo>();
    public DbSet<ClienteDocumento> ClienteDocumentos => Set<ClienteDocumento>();
    public DbSet<ClientePessoaFisica> ClientesPessoaFisica => Set<ClientePessoaFisica>();
    public DbSet<ClientePessoaJuridica> ClientesPessoaJuridica => Set<ClientePessoaJuridica>();
    #endregion

    #region Veiculo
    public DbSet<VeiculoMarca> VeiculoMarcas => Set<VeiculoMarca>();
    public DbSet<VeiculoModelo> VeiculoModelos => Set<VeiculoModelo>();
    public DbSet<VeiculoCliente> VeiculosClientes => Set<VeiculoCliente>();
    #endregion

    #region Mecanico
    public DbSet<Mecanico> Mecanicos => Set<Mecanico>();
    public DbSet<MecanicoEspecialidade> MecanicoEspecialidades => Set<MecanicoEspecialidade>();
    public DbSet<MecanicoEspecialidadeRel> MecanicoEspecialidadesRel => Set<MecanicoEspecialidadeRel>();
    public DbSet<MecanicoDocumento> MecanicoDocumentos => Set<MecanicoDocumento>();
    public DbSet<MecanicoContato> MecanicoContatos => Set<MecanicoContato>();
    public DbSet<MecanicoEndereco> MecanicoEnderecos => Set<MecanicoEndereco>();
    public DbSet<MecanicoCertificacao> MecanicoCertificacoes => Set<MecanicoCertificacao>();
    public DbSet<MecanicoDisponibilidade> MecanicoDisponibilidades => Set<MecanicoDisponibilidade>();
    public DbSet<MecanicoExperiencia> MecanicoExperiencias => Set<MecanicoExperiencia>();
    #endregion

    #region Fornecedor
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cad_clientes");
            entity.Property(p => p.Nome).HasMaxLength(160).IsRequired();
            entity.Property(p => p.NomeExibicao).HasMaxLength(160);
            entity.Property(p => p.Documento).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Telefone).HasMaxLength(20);
            entity.Property(p => p.Email).HasMaxLength(160);
            entity.HasIndex(p => p.Documento).IsUnique();

            entity.HasOne(p => p.Origem).WithMany(o => o.Clientes).HasForeignKey(p => p.Origem_Id).OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(p => p.Enderecos).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Contatos).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Indicacoes).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Veiculos).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.PessoaFisica).WithOne().HasForeignKey<ClientePessoaFisica>(pf => pf.Cliente_Id).HasPrincipalKey<Cliente>(c => c.Id).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            entity.HasOne(p => p.PessoaJuridica).WithOne().HasForeignKey<ClientePessoaJuridica>(pj => pj.Cliente_Id).HasPrincipalKey<Cliente>(c => c.Id).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            entity.HasMany(p => p.Anexos).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Documentos).WithOne(e => e.Cliente).HasForeignKey(e => e.Cliente_Id).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClienteOrigem>(entity =>
        {
            entity.ToTable("cad_clientes_origens");
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Descricao).HasMaxLength(240);
            entity.HasIndex(p => p.Nome).IsUnique();
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

        modelBuilder.Entity<ClientePessoaFisica>(entity =>
        {
            entity.ToTable("cad_clientes_pf");
            entity.Property(p => p.Cpf).HasMaxLength(14).IsRequired();
            entity.Property(p => p.Rg).HasMaxLength(20);
            entity.Property(p => p.Genero).HasMaxLength(30);
            entity.Property(p => p.Estado_Civil).HasMaxLength(20);
            entity.Property(p => p.Profissao).HasMaxLength(120);
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.Property(p => p.Cliente_Id).IsRequired();
        });

        modelBuilder.Entity<ClientePessoaJuridica>(entity =>
        {
            entity.ToTable("cad_clientes_pj");
            entity.Property(p => p.Cnpj).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Razao_Social).HasMaxLength(180).IsRequired();
            entity.Property(p => p.Nome_Fantasia).HasMaxLength(180);
            entity.Property(p => p.Inscricao_Estadual).HasMaxLength(30);
            entity.Property(p => p.Inscricao_Municipal).HasMaxLength(30);
            entity.Property(p => p.Responsavel).HasMaxLength(120);
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.Property(p => p.Cliente_Id).IsRequired();
        });

        modelBuilder.Entity<ClienteConsentimento>(entity =>
        {
            entity.ToTable("cad_clientes_lgpd_consentimentos");
            entity.Property(p => p.Canal).HasMaxLength(80).IsRequired();
            entity.Property(p => p.Observacoes).HasMaxLength(240);
            entity.HasIndex(p => p.Cliente_Id).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithOne(c => c.Consentimento)
                .HasForeignKey<ClienteConsentimento>(p => p.Cliente_Id)
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

        modelBuilder.Entity<ClienteAnexo>(entity =>
        {
            entity.ToTable("cad_clientes_anexos");
            entity.Property(p => p.Nome).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Tipo).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Url).HasMaxLength(500).IsRequired();
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => new { p.Cliente_Id, p.Nome }).IsUnique();
        });

        modelBuilder.Entity<ClienteDocumento>(entity =>
        {
            entity.ToTable("cad_clientes_documentos");
            entity.Property(p => p.Tipo).HasMaxLength(30).IsRequired();
            entity.Property(p => p.Documento).HasMaxLength(40).IsRequired();
            entity.Property(p => p.Orgao_Expedidor).HasMaxLength(80);
            entity.Property(p => p.Principal).HasDefaultValue(false);
            entity.HasIndex(p => new { p.Cliente_Id, p.Tipo, p.Documento }).IsUnique();
            entity.HasOne(p => p.Cliente)
                .WithMany(c => c.Documentos)
                .HasForeignKey(p => p.Cliente_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
        #endregion

        #region Veiculo
        modelBuilder.Entity<VeiculoCliente>(entity =>
        {
            entity.ToTable("cad_veiculos");
            entity.Property(p => p.Placa).HasMaxLength(10).IsRequired();
            entity.Property(p => p.Cor).HasMaxLength(40);
            entity.Property(p => p.Renavam).HasMaxLength(20);
            entity.Property(p => p.Chassi).HasMaxLength(40);
            entity.Property(p => p.Combustivel).HasMaxLength(40);
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => p.Placa).IsUnique();
            entity.HasIndex(p => p.Renavam).IsUnique().HasFilter("Renavam IS NOT NULL");
            entity.HasOne(p => p.Modelo).WithMany(m => m.Veiculos).HasForeignKey(p => p.Modelo_Id).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
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
            entity.HasOne(p => p.Marca).WithMany(m => m.Modelos).HasForeignKey(p => p.Marca_Id).OnDelete(DeleteBehavior.Cascade);
        });
        #endregion

        #region Mecanico
        modelBuilder.Entity<Mecanico>(entity =>
        {
            entity.ToTable("cad_mecanicos");
            entity.Property(p => p.Codigo).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Sobrenome).HasMaxLength(120);
            entity.Property(p => p.Nome_Social).HasMaxLength(120);
            entity.Property(p => p.Documento_Principal).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Status).HasMaxLength(20).HasDefaultValue("Ativo");
            entity.Property(p => p.Nivel).HasMaxLength(20).HasDefaultValue("Pleno");
            entity.Property(p => p.Observacoes).HasMaxLength(500);
            entity.Property(p => p.Valor_Hora).HasColumnType("decimal(10,2)");
            entity.Property(p => p.Carga_Horaria_Semanal).HasDefaultValue(44);
            entity.HasIndex(p => p.Codigo).IsUnique();
            entity.HasIndex(p => p.Documento_Principal).IsUnique();
            entity.HasOne(p => p.EspecialidadePrincipal)
                .WithMany()
                .HasForeignKey(p => p.Especialidade_Principal_Id)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(p => p.Especialidades)
                .WithOne(e => e.Mecanico)
                .HasForeignKey(e => e.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Documentos)
                .WithOne(d => d.Mecanico)
                .HasForeignKey(d => d.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Contatos)
                .WithOne(c => c.Mecanico)
                .HasForeignKey(c => c.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Enderecos)
                .WithOne(e => e.Mecanico)
                .HasForeignKey(e => e.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Certificacoes)
                .WithOne(c => c.Mecanico)
                .HasForeignKey(c => c.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Disponibilidades).WithOne(d => d.Mecanico).HasForeignKey(d => d.Mecanico_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Experiencias).WithOne(e => e.Mecanico).HasForeignKey(e => e.Mecanico_Id).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoEspecialidade>(entity =>
        {
            entity.ToTable("cad_mecanicos_especialidades");
            entity.Property(p => p.Codigo).HasMaxLength(16).IsRequired();
            entity.Property(p => p.Nome).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Descricao).HasMaxLength(240);
            entity.HasIndex(p => p.Codigo).IsUnique();
            entity.HasIndex(p => p.Nome).IsUnique();
        });

        modelBuilder.Entity<MecanicoEspecialidadeRel>(entity =>
        {
            entity.ToTable("cad_mecanicos_especialidades_rel");
            entity.Property(p => p.Nivel).HasMaxLength(20).HasDefaultValue("Pleno");
            entity.Property(p => p.Anotacoes).HasMaxLength(240);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Especialidade_Id }).IsUnique();
            entity.HasIndex(p => p.Especialidade_Id);
            entity.HasOne(p => p.Mecanico)
                .WithMany(m => m.Especialidades)
                .HasForeignKey(p => p.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(p => p.Especialidade)
                .WithMany(e => e.Mecanicos)
                .HasForeignKey(p => p.Especialidade_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoDocumento>(entity =>
        {
            entity.ToTable("cad_mecanicos_documentos");
            entity.Property(p => p.Tipo).HasMaxLength(30).IsRequired();
            entity.Property(p => p.Numero).HasMaxLength(40).IsRequired();
            entity.Property(p => p.Orgao_Expedidor).HasMaxLength(80);
            entity.Property(p => p.Arquivo_Url).HasMaxLength(240);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Tipo, p.Numero }).IsUnique();
            entity.HasOne(p => p.Mecanico)
                .WithMany(m => m.Documentos)
                .HasForeignKey(p => p.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoContato>(entity =>
        {
            entity.ToTable("cad_mecanicos_contatos");
            entity.Property(p => p.Tipo).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Valor).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Observacao).HasMaxLength(240);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Tipo, p.Valor }).IsUnique();
            entity.HasOne(p => p.Mecanico)
                .WithMany(m => m.Contatos)
                .HasForeignKey(p => p.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoEndereco>(entity =>
        {
            entity.ToTable("cad_mecanicos_enderecos");
            entity.Property(p => p.Tipo).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Cep).HasMaxLength(12).IsRequired();
            entity.Property(p => p.Logradouro).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Numero).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Bairro).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Cidade).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Estado).HasMaxLength(60).IsRequired();
            entity.Property(p => p.Pais).HasMaxLength(80);
            entity.Property(p => p.Complemento).HasMaxLength(120);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Tipo, p.Principal }).IsUnique();
            entity.HasOne(p => p.Mecanico)
                .WithMany(m => m.Enderecos)
                .HasForeignKey(p => p.Mecanico_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoCertificacao>(entity =>
        {
            entity.ToTable("cad_mecanicos_certificacoes");
            entity.Property(p => p.Titulo).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Instituicao).HasMaxLength(160);
            entity.Property(p => p.Codigo_Certificacao).HasMaxLength(60);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Titulo }).IsUnique();
            entity.HasIndex(p => p.Especialidade_Id);
            entity.HasOne(p => p.Mecanico).WithMany(m => m.Certificacoes).HasForeignKey(p => p.Mecanico_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(p => p.Especialidade).WithMany(e => e.Certificacoes).HasForeignKey(p => p.Especialidade_Id).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<MecanicoDisponibilidade>(entity =>
        {
            entity.ToTable("cad_mecanicos_disponibilidades");
            entity.Property(p => p.Dia_Semana).IsRequired();
            entity.Property(p => p.Hora_Inicio).HasColumnType("time");
            entity.Property(p => p.Hora_Fim).HasColumnType("time");
            entity.HasIndex(p => new { p.Mecanico_Id, p.Dia_Semana, p.Hora_Inicio, p.Hora_Fim }).IsUnique();
            entity.HasOne(p => p.Mecanico).WithMany(m => m.Disponibilidades).HasForeignKey(p => p.Mecanico_Id).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MecanicoExperiencia>(entity =>
        {
            entity.ToTable("cad_mecanicos_experiencias");
            entity.Property(p => p.Empresa).HasMaxLength(160).IsRequired();
            entity.Property(p => p.Cargo).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Resumo_Atividades).HasMaxLength(400);
            entity.HasIndex(p => new { p.Mecanico_Id, p.Empresa, p.Cargo }).IsUnique();
            entity.HasOne(p => p.Mecanico).WithMany(m => m.Experiencias).HasForeignKey(p => p.Mecanico_Id).OnDelete(DeleteBehavior.Cascade);
        });
        #endregion

        #region Fornecedor
        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.ToTable("cad_fornecedor");
            entity.Property(p => p.Razao_Social).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Nome_Fantasia).HasMaxLength(200);
            entity.Property(p => p.Cnpj).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Inscricao_Estadual).HasMaxLength(30);
            entity.Property(p => p.Contato).HasMaxLength(100);
            entity.Property(p => p.Email).HasMaxLength(100);
            entity.Property(p => p.Telefone).HasMaxLength(30);
            entity.Property(p => p.Observacoes).HasMaxLength(500);
            entity.Property(p => p.Status).HasMaxLength(20).HasDefaultValue("Ativo");
            entity.HasIndex(p => p.Cnpj).IsUnique();

            entity.HasMany(p => p.Enderecos).WithOne(e => e.Fornecedor).HasForeignKey(e => e.Fornecedor_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Contatos).WithOne(c => c.Fornecedor).HasForeignKey(c => c.Fornecedor_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Anexos).WithOne(a => a.Fornecedor).HasForeignKey(a => a.Fornecedor_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Bancos).WithOne(b => b.Fornecedor).HasForeignKey(b => b.Fornecedor_Id).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.Historicos).WithOne(h => h.Fornecedor).HasForeignKey(h => h.Fornecedor_Id).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FornecedorEndereco>(entity =>
        {
            entity.ToTable("cad_fornecedor_endereco");
            entity.Property(p => p.Tipo).HasMaxLength(50);
            entity.Property(p => p.Cep).HasMaxLength(20);
            entity.Property(p => p.Logradouro).HasMaxLength(200);
            entity.Property(p => p.Numero).HasMaxLength(20);
            entity.Property(p => p.Bairro).HasMaxLength(100);
            entity.Property(p => p.Cidade).HasMaxLength(100);
            entity.Property(p => p.Estado).HasMaxLength(50);
            entity.Property(p => p.Pais).HasMaxLength(50);
            entity.Property(p => p.Complemento).HasMaxLength(100);
            entity.Property(p => p.Principal).HasDefaultValue(false);
        });

        modelBuilder.Entity<FornecedorContato>(entity =>
        {
            entity.ToTable("cad_fornecedor_contato");
            entity.Property(p => p.Tipo).HasMaxLength(50);
            entity.Property(p => p.Valor).HasMaxLength(100);
            entity.Property(p => p.Principal).HasDefaultValue(false);
            entity.Property(p => p.Observacao).HasMaxLength(200);
        });

        modelBuilder.Entity<FornecedorAnexo>(entity =>
        {
            entity.ToTable("cad_fornecedor_anexo");
            entity.Property(p => p.Nome).HasMaxLength(200);
            entity.Property(p => p.Tipo).HasMaxLength(50);
            entity.Property(p => p.Url).HasMaxLength(500);
            entity.Property(p => p.Observacao).HasMaxLength(200);
            entity.Property(p => p.Data_Upload).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<FornecedorBanco>(entity =>
        {
            entity.ToTable("cad_fornecedor_banco");
            entity.Property(p => p.Banco).HasMaxLength(100);
            entity.Property(p => p.Agencia).HasMaxLength(20);
            entity.Property(p => p.Conta).HasMaxLength(30);
            entity.Property(p => p.Tipo_Conta).HasMaxLength(20);
            entity.Property(p => p.Titular).HasMaxLength(100);
            entity.Property(p => p.Documento_Titular).HasMaxLength(30);
            entity.Property(p => p.Pix_Chave).HasMaxLength(100);
        });

        modelBuilder.Entity<FornecedorHistorico>(entity =>
        {
            entity.ToTable("cad_fornecedor_historico");
            entity.Property(p => p.Data_Alteracao).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(p => p.Usuario).HasMaxLength(100);
            entity.Property(p => p.Campo).HasMaxLength(100);
            entity.Property(p => p.Valor_Antigo).HasColumnType("text");
            entity.Property(p => p.Valor_Novo).HasColumnType("text");
        });
        #endregion
    }
}

