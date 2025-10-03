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
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Codigo = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 180, nullable: false),
                    Tipo = table.Column<string>(maxLength: 32, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    Origem = table.Column<string>(maxLength: 32, nullable: false),
                    Vip = table.Column<bool>(nullable: false),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_Codigo",
                table: "cad_clientes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateTable(
                name: "cad_clientes_pf",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Cpf = table.Column<string>(maxLength: 14, nullable: false),
                    Rg = table.Column<string>(maxLength: 20, nullable: true),
                    Data_Nascimento = table.Column<DateTime>(nullable: true),
                    Genero = table.Column<string>(maxLength: 30, nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pf_Cliente_Id",
                table: "cad_clientes_pf",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pf_Cpf",
                table: "cad_clientes_pf",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateTable(
                name: "cad_clientes_pj",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Cnpj = table.Column<string>(maxLength: 18, nullable: false),
                    Razao_Social = table.Column<string>(maxLength: 180, nullable: false),
                    Nome_Fantasia = table.Column<string>(maxLength: 180, nullable: true),
                    Inscricao_Estadual = table.Column<string>(maxLength: 30, nullable: true),
                    Responsavel = table.Column<string>(maxLength: 120, nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pj_Cliente_Id",
                table: "cad_clientes_pj",
                column: "Cliente_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_pj_Cnpj",
                table: "cad_clientes_pj",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateTable(
                name: "cad_clientes_enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 32, nullable: false),
                    Cep = table.Column<string>(maxLength: 12, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 160, nullable: false),
                    Numero = table.Column<string>(maxLength: 20, nullable: false),
                    Bairro = table.Column<string>(maxLength: 120, nullable: false),
                    Cidade = table.Column<string>(maxLength: 120, nullable: false),
                    Estado = table.Column<string>(maxLength: 60, nullable: false),
                    Pais = table.Column<string>(maxLength: 60, nullable: false),
                    Complemento = table.Column<string>(maxLength: 160, nullable: true),
                    Principal = table.Column<bool>(nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_enderecos_Cliente_Id",
                table: "cad_clientes_enderecos",
                column: "Cliente_Id");

            migrationBuilder.CreateTable(
                name: "cad_clientes_contatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 32, nullable: false),
                    Valor = table.Column<string>(maxLength: 120, nullable: false),
                    Principal = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 200, nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_contatos_Cliente_Id",
                table: "cad_clientes_contatos",
                column: "Cliente_Id");

            migrationBuilder.CreateTable(
                name: "cad_clientes_consentimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 40, nullable: false),
                    Aceito = table.Column<bool>(nullable: false),
                    Data = table.Column<DateTime>(nullable: true),
                    Valido_Ate = table.Column<DateTime>(nullable: true),
                    Observacoes = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_consentimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_consentimentos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_consentimentos_Cliente_Id",
                table: "cad_clientes_consentimentos",
                column: "Cliente_Id");

            migrationBuilder.CreateTable(
                name: "cad_clientes_veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Placa = table.Column<string>(maxLength: 10, nullable: false),
                    Marca = table.Column<string>(maxLength: 80, nullable: true),
                    Modelo = table.Column<string>(maxLength: 120, nullable: true),
                    Ano = table.Column<int>(nullable: true),
                    Cor = table.Column<string>(maxLength: 60, nullable: true),
                    Chassi = table.Column<string>(maxLength: 30, nullable: true),
                    Principal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_veiculos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_veiculos_Cliente_Id",
                table: "cad_clientes_veiculos",
                column: "Cliente_Id");

            migrationBuilder.CreateTable(
                name: "cad_clientes_anexos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 160, nullable: false),
                    Tipo = table.Column<string>(maxLength: 80, nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    Observacao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_anexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cad_clientes_anexos_cad_clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "cad_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_clientes_anexos_Cliente_Id",
                table: "cad_clientes_anexos",
                column: "Cliente_Id");

            migrationBuilder.CreateTable(
                name: "cad_mecanicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 120, nullable: false),
                    Especialidade = table.Column<string>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_mecanicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cad_fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Razao_Social = table.Column<string>(maxLength: 160, nullable: false),
                    Cnpj = table.Column<string>(maxLength: 20, nullable: false),
                    Contato = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_fornecedores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_fornecedores_Cnpj",
                table: "cad_fornecedores",
                column: "Cnpj",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "cad_clientes_anexos");
            migrationBuilder.DropTable(name: "cad_clientes_consentimentos");
            migrationBuilder.DropTable(name: "cad_clientes_contatos");
            migrationBuilder.DropTable(name: "cad_clientes_enderecos");
            migrationBuilder.DropTable(name: "cad_clientes_pf");
            migrationBuilder.DropTable(name: "cad_clientes_pj");
            migrationBuilder.DropTable(name: "cad_clientes_veiculos");
            migrationBuilder.DropTable(name: "cad_fornecedores");
            migrationBuilder.DropTable(name: "cad_mecanicos");
            migrationBuilder.DropTable(name: "cad_clientes");
        }
    }
}
