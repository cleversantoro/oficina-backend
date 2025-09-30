using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "oficina");

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS pgcrypto;");

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "oficina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Endereco = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pecas",
                schema: "oficina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Codigo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pecas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profissionais",
                schema: "oficina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Especialidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "servicos",
                schema: "oficina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    PrecoBase = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicos", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "ordens_servico",
            //    schema: "oficina",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
            //        MotoId = table.Column<Guid>(type: "uuid", nullable: false),
            //        ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
            //        ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
            //        Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
            //        DataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        DataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //        Total = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ordens_servico", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ordens_servico_clientes_ClienteId",
            //            column: x => x.ClienteId,
            //            principalSchema: "oficina",
            //            principalTable: "clientes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ordens_servico_motos_MotoId",
            //            column: x => x.MotoId,
            //            principalSchema: "oficina",
            //            principalTable: "motos",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ordens_servico_profissionais_ProfissionalId",
            //            column: x => x.ProfissionalId,
            //            principalSchema: "oficina",
            //            principalTable: "profissionais",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "motos",
                schema: "oficina",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Marca = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    placa = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Chassi = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    KmAtual = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_motos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "oficina",
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "itens_ordem_servico",
            //    schema: "oficina",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
            //        OrdemServicoId = table.Column<Guid>(type: "uuid", nullable: false),
            //        ServicoId = table.Column<Guid>(type: "uuid", nullable: true),
            //        PecaId = table.Column<Guid>(type: "uuid", nullable: true),
            //        Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
            //        Quantidade = table.Column<decimal>(type: "numeric(12,3)", nullable: false),
            //        Preco = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        //table.PrimaryKey("PK_itens_ordem_servico", x => x.Id);
            //        //table.ForeignKey(
            //        //    name: "FK_itens_ordem_servico_ordens_servico_OrdemServicoId",
            //        //    column: x => x.OrdemServicoId,
            //        //    principalSchema: "oficina",
            //        //    principalTable: "ordens_servico",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Cascade);
            //        //table.ForeignKey(
            //        //    name: "FK_itens_ordem_servico_pecas_PecaId",
            //        //    column: x => x.PecaId,
            //        //    principalSchema: "oficina",
            //        //    principalTable: "pecas",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Restrict);
            //        //table.ForeignKey(
            //        //    name: "FK_itens_ordem_servico_servicos_ServicoId",
            //        //    column: x => x.ServicoId,
            //        //    principalSchema: "oficina",
            //        //    principalTable: "servicos",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_Documento",
                schema: "oficina",
                table: "clientes",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_motos_ClienteId",
                schema: "oficina",
                table: "motos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_motos_placa",
                schema: "oficina",
                table: "motos",
                column: "placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pecas_Codigo",
                schema: "oficina",
                table: "pecas",
                column: "Codigo",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_itens_ordem_servico_OrdemServicoId",
            //    schema: "oficina",
            //    table: "itens_ordem_servico",
            //    column: "OrdemServicoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_itens_ordem_servico_PecaId",
            //    schema: "oficina",
            //    table: "itens_ordem_servico",
            //    column: "PecaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_itens_ordem_servico_ServicoId",
            //    schema: "oficina",
            //    table: "itens_ordem_servico",
            //    column: "ServicoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ordens_servico_ClienteId",
            //    schema: "oficina",
            //    table: "ordens_servico",
            //    column: "ClienteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ordens_servico_MotoId",
            //    schema: "oficina",
            //    table: "ordens_servico",
            //    column: "MotoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ordens_servico_ProfissionalId",
            //    schema: "oficina",
            //    table: "ordens_servico",
            //    column: "ProfissionalId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ordens_servico_DataAbertura_Status",
            //    schema: "oficina",
            //    table: "ordens_servico",
            //    columns: new[] { "DataAbertura", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "itens_ordem_servico",
            //    schema: "oficina");

            //migrationBuilder.DropTable(
            //    name: "ordens_servico",
            //    schema: "oficina");

            migrationBuilder.DropTable(
                name: "motos",
                schema: "oficina");

            migrationBuilder.DropTable(
                name: "pecas",
                schema: "oficina");

            migrationBuilder.DropTable(
                name: "profissionais",
                schema: "oficina");

            migrationBuilder.DropTable(
                name: "servicos",
                schema: "oficina");

            migrationBuilder.DropTable(
                name: "clientes",
                schema: "oficina");
        }
    }
}
