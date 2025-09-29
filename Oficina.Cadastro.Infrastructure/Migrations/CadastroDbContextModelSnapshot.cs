using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Oficina.Cadastro.Infrastructure.Persistence;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    [DbContext(typeof(CadastroDbContext))]
    partial class CadastroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.HasDefaultSchema("oficina");

            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Cliente", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<DateTime>("CriadoEm")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("Documento")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("character varying(20)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)")
                    .HasColumnName("email");

                b.Property<string>("Endereco")
                    .HasMaxLength(300)
                    .HasColumnType("character varying(300)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(160)
                    .HasColumnType("character varying(160)");

                b.Property<string>("Telefone")
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnType("character varying(12)")
                    .HasColumnName("telefone");

                b.HasKey("Id");

                b.HasIndex("Documento")
                    .IsUnique();

                b.ToTable("clientes", "oficina");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Moto", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<int>("Ano")
                    .HasColumnType("integer");

                b.Property<Guid>("ClienteId")
                    .HasColumnType("uuid");

                b.Property<string>("Chassi")
                    .HasMaxLength(30)
                    .HasColumnType("character varying(30)");

                b.Property<int?>("KmAtual")
                    .HasColumnType("integer");

                b.Property<string>("Marca")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("character varying(80)");

                b.Property<string>("Modelo")
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnType("character varying(120)");

                b.Property<string>("Placa")
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnType("character varying(8)")
                    .HasColumnName("placa");

                b.HasKey("Id");

                b.HasIndex("ClienteId");

                b.HasIndex("Placa")
                    .IsUnique()
                    .HasDatabaseName("IX_motos_placa");

                b.ToTable("motos", "oficina");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Peca", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Codigo")
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnType("character varying(60)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<decimal>("Preco")
                    .HasColumnType("numeric(12,2)");

                b.HasKey("Id");

                b.HasIndex("Codigo")
                    .IsUnique();

                b.ToTable("pecas", "oficina");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Profissional", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Especialidade")
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)");

                b.HasKey("Id");

                b.ToTable("profissionais", "oficina");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Servico", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasDefaultValueSql("gen_random_uuid()");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnType("character varying(120)");

                b.Property<decimal>("PrecoBase")
                    .HasColumnType("numeric(12,2)");

                b.HasKey("Id");

                b.ToTable("servicos", "oficina");
            });

            modelBuilder.Entity("Oficina.Cadastro.Domain.Entities.Moto", b =>
            {
                b.HasOne("Oficina.Cadastro.Domain.Entities.Cliente", "Cliente")
                    .WithMany()
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Cliente");
            });
#pragma warning restore 612, 618
        }
    }
}
