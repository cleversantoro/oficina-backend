using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_origens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_origens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_fornecedores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Razao_Social = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contato = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_fornecedores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_mecanicos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Especialidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_mecanicos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_veiculos_marcas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pais = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_veiculos_marcas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeExibicao = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Documento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Vip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Observacoes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrigemCadastroId = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Origem_Id = table.Column<long>(type: "bigint", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_cad_clientes_origens_Origem_Id",
                        column: x => x.Origem_Id,
                        principalTable: "cad_clientes_origens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_veiculos_modelos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca_Id = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ano_Inicio = table.Column<int>(type: "int", nullable: true),
                    Ano_Fim = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_veiculos_modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_veiculos_modelos_cad_veiculos_marcas_Marca_Id",
                        column: x => x.Marca_Id,
                        principalTable: "cad_veiculos_marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_anexos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_anexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_anexos_cad_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "cad_clientes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_contatos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<int>(type: "int", maxLength: 40, nullable: false),
                    Valor = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Principal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Observacao = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_contatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_contatos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_enderecos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<int>(type: "int", maxLength: 40, nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complemento = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pais = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Principal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_enderecos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_financeiro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Limite_Credito = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Prazo_Pagamento = table.Column<int>(type: "int", nullable: true),
                    Bloqueado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_financeiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_financeiro_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_indicacoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Indicador_Nome = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Indicador_Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_indicacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_indicacoes_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_lgpd_consentimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Aceito = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Valido_Ate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Observacoes = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Canal = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_lgpd_consentimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_lgpd_consentimentos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_pf",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rg = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Genero = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado_Civil = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profissao = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_pf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_pf_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_clientes_pj",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Razao_Social = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome_Fantasia = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Inscricao_Estadual = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Inscricao_Municipal = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsavel = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_pj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_pj_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cad_veiculos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente_Id = table.Column<long>(type: "bigint", nullable: false),
                    Placa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ano = table.Column<int>(type: "int", nullable: true),
                    Cor = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Chassi = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Principal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Modelo_Id = table.Column<long>(type: "bigint", nullable: true),
                    Renavam = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Combustivel = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_veiculos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cad_veiculos_cad_veiculos_modelos_Modelo_Id",
                        column: x => x.Modelo_Id,
                        principalTable: "cad_veiculos_modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_Documento",
                table: "cad_clientes",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_Origem_Id",
                table: "cad_clientes",
                column: "Origem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_anexos_Cliente_Id_Nome",
                table: "cad_clientes_anexos",
                columns: new[] { "Cliente_Id", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_anexos_ClienteId",
                table: "cad_clientes_anexos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_contatos_Cliente_Id_Tipo_Valor",
                table: "cad_clientes_contatos",
                columns: new[] { "Cliente_Id", "Tipo", "Valor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_enderecos_Cliente_Id_Tipo_Principal",
                table: "cad_clientes_enderecos",
                columns: new[] { "Cliente_Id", "Tipo", "Principal" });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_financeiro_Cliente_Id",
                table: "cad_clientes_financeiro",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_indicacoes_Cliente_Id",
                table: "cad_clientes_indicacoes",
                column: "Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_lgpd_consentimentos_Cliente_Id",
                table: "cad_clientes_lgpd_consentimentos",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_origens_Nome",
                table: "cad_clientes_origens",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pf_Cliente_Id",
                table: "cad_clientes_pf",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pj_Cliente_Id",
                table: "cad_clientes_pj",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_fornecedores_Cnpj",
                table: "cad_fornecedores",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_Cliente_Id",
                table: "cad_veiculos",
                column: "Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_Modelo_Id",
                table: "cad_veiculos",
                column: "Modelo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_Placa",
                table: "cad_veiculos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_Renavam",
                table: "cad_veiculos",
                column: "Renavam",
                unique: true,
                filter: "Renavam IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_marcas_Nome",
                table: "cad_veiculos_marcas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_veiculos_modelos_Marca_Id_Nome",
                table: "cad_veiculos_modelos",
                columns: new[] { "Marca_Id", "Nome" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cad_clientes_anexos");

            migrationBuilder.DropTable(
                name: "cad_clientes_contatos");

            migrationBuilder.DropTable(
                name: "cad_clientes_enderecos");

            migrationBuilder.DropTable(
                name: "cad_clientes_financeiro");

            migrationBuilder.DropTable(
                name: "cad_clientes_indicacoes");

            migrationBuilder.DropTable(
                name: "cad_clientes_lgpd_consentimentos");

            migrationBuilder.DropTable(
                name: "cad_clientes_pf");

            migrationBuilder.DropTable(
                name: "cad_clientes_pj");

            migrationBuilder.DropTable(
                name: "cad_fornecedores");

            migrationBuilder.DropTable(
                name: "cad_mecanicos");

            migrationBuilder.DropTable(
                name: "cad_veiculos");

            migrationBuilder.DropTable(
                name: "cad_clientes");

            migrationBuilder.DropTable(
                name: "cad_veiculos_modelos");

            migrationBuilder.DropTable(
                name: "cad_clientes_origens");

            migrationBuilder.DropTable(
                name: "cad_veiculos_marcas");
        }
    }
}
