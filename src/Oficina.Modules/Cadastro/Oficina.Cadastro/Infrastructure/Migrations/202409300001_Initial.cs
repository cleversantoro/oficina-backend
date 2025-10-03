using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oficina.Cadastro.Domain;
#nullable disable
namespace Oficina.Cadastro.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cad_clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    tenant_id = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(maxLength: 120, nullable: false),
                    nome_exibicao = table.Column<string>(maxLength: 160, nullable: false),
                    documento = table.Column<string>(maxLength: 20, nullable: false),
                    pessoa_tipo = table.Column<short>(nullable: false),
                    status = table.Column<short>(nullable: false, defaultValue: (short)ClienteStatus.Ativo),
                    cliente_vip = table.Column<bool>(nullable: false, defaultValue: false),
                    origem_cadastro_id = table.Column<int>(nullable: false),
                    telefone = table.Column<string>(maxLength: 20, nullable: false),
                    email = table.Column<string>(maxLength: 160, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)"),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ux_cad_clientes_documento",
                table: "cad_clientes",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cad_clientes_status",
                table: "cad_clientes",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_cad_clientes_origem",
                table: "cad_clientes",
                column: "origem_cadastro_id");

            migrationBuilder.CreateTable(
                name: "cad_mecanicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    Nome = table.Column<string>(maxLength:120, nullable:false),
                    Especialidade = table.Column<string>(nullable:false),
                    Ativo = table.Column<bool>(nullable:false)
                },
                constraints: table => { table.PrimaryKey("PK_cad_mecanicos", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "cad_fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    RazaoSocial = table.Column<string>(maxLength:160, nullable:false),
                    Cnpj = table.Column<string>(maxLength:20, nullable:false),
                    Contato = table.Column<string>(nullable:false)
                },
                constraints: table => { table.PrimaryKey("PK_cad_fornecedores", x => x.Id); });
            migrationBuilder.CreateIndex(name:"IX_cad_fornecedores_Cnpj", table:"cad_fornecedores", column:"Cnpj", unique:true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cad_clientes");

            migrationBuilder.DropTable(
                name: "cad_mecanicos");

            migrationBuilder.DropTable(
                name: "cad_fornecedores");
        }
    }
}
