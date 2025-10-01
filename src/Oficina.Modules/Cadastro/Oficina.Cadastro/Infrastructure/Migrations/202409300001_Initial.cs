using Microsoft.EntityFrameworkCore.Migrations;
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
                    Id = table.Column<Guid>(nullable:false),
                    CreatedAt = table.Column<DateTime>(nullable:false),
                    UpdatedAt = table.Column<DateTime>(nullable:true),
                    Nome = table.Column<string>(maxLength:120, nullable:false),
                    Documento = table.Column<string>(maxLength:20, nullable:false),
                    Telefone = table.Column<string>(nullable:false),
                    Email = table.Column<string>(nullable:false)
                },
                constraints: table => { table.PrimaryKey("PK_cad_clientes", x => x.Id); });
            migrationBuilder.CreateIndex(name:"IX_cad_clientes_Documento", table:"cad_clientes", column:"Documento", unique:true);

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
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
