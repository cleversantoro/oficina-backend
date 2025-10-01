using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Oficina.Estoque.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "est_pecas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    Codigo = table.Column<string>(maxLength:40, nullable:false),
                    Descricao = table.Column<string>(maxLength:200, nullable:false),
                    PrecoUnitario = table.Column<decimal>(type:"decimal(18,2)", nullable:false),
                    Quantidade = table.Column<int>(nullable:false),
                    FornecedorId = table.Column<Guid>(nullable:true)
                },
                constraints: table => { table.PrimaryKey("PK_est_pecas", x => x.Id); });
            migrationBuilder.CreateIndex(name:"IX_est_pecas_Codigo", table:"est_pecas", column:"Codigo", unique:true);

            migrationBuilder.CreateTable(
                name: "est_movimentacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    PecaId = table.Column<Guid>(nullable:false),
                    Quantidade = table.Column<int>(nullable:false),
                    Tipo = table.Column<string>(maxLength:10, nullable:false),
                    Referencia = table.Column<string>(nullable:true)
                },
                constraints: table => { table.PrimaryKey("PK_est_movimentacoes", x => x.Id); });
        }
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
