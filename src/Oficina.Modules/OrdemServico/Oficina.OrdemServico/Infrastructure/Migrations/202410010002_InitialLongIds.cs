using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.OrdemServico.Infrastructure.Migrations
{
    public partial class InitialLongIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "os_ordens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cliente_Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: false),
                    Mecanico_Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: false),
                    Descricao_Problema = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data_Abertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Data_Conclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_os_ordens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "os_itens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created_At = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Ordem_Servico_Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: false),
                    Peca_Id = table.Column<long>(type: "BIGINT UNSIGNED", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Valor_Unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_os_itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_os_itens_os_ordens_Ordem_Servico_Id",
                        column: x => x.Ordem_Servico_Id,
                        principalTable: "os_ordens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_os_itens_Ordem_Servico_Id",
                table: "os_itens",
                column: "Ordem_Servico_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "os_itens");

            migrationBuilder.DropTable(
                name: "os_ordens");
        }
    }
}
