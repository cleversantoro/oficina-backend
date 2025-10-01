using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Oficina.OrdemServico.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "os_ordens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    ClienteId = table.Column<Guid>(nullable:false),
                    MecanicoId = table.Column<Guid>(nullable:false),
                    DescricaoProblema = table.Column<string>(nullable:false),
                    Status = table.Column<string>(maxLength:20, nullable:false),
                    DataAbertura = table.Column<DateTime>(nullable:false),
                    DataConclusao = table.Column<DateTime>(nullable:true)
                },
                constraints: table => { table.PrimaryKey("PK_os_ordens", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "os_itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    OrdemServicoId = table.Column<Guid>(nullable:false),
                    PecaId = table.Column<Guid>(nullable:true),
                    Descricao = table.Column<string>(maxLength:200, nullable:false),
                    Quantidade = table.Column<int>(nullable:false),
                    ValorUnitario = table.Column<decimal>(type:"decimal(18,2)", nullable:false)
                },
                constraints: table => { table.PrimaryKey("PK_os_itens", x => x.Id); });
        }
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
