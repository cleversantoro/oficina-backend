using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Oficina.Financeiro.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fin_pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    OrdemServicoId = table.Column<Guid>(nullable:false),
                    Meio = table.Column<string>(maxLength:12, nullable:false),
                    Valor = table.Column<decimal>(type:"decimal(18,2)", nullable:false),
                    Status = table.Column<string>(maxLength:12, nullable:false),
                    TransacaoId = table.Column<string>(nullable:true)
                },
                constraints: table => { table.PrimaryKey("PK_fin_pagamentos", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "fin_nfes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    OrdemServicoId = table.Column<Guid>(nullable:false),
                    Numero = table.Column<string>(maxLength:20, nullable:false),
                    ChaveAcesso = table.Column<string>(maxLength:60, nullable:false),
                    Status = table.Column<string>(nullable:false)
                },
                constraints: table => { table.PrimaryKey("PK_fin_nfes", x => x.Id); });
        }
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
