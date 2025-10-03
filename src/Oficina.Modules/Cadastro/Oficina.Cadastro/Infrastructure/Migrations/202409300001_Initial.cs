using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Cadastro.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cad_clientes_origens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 120, nullable: false),
                    Descricao = table.Column<string>(maxLength: 240, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_clientes_origens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cad_clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 160, nullable: false),
                    Documento = table.Column<string>(maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 160, nullable: false),
                    Origem_Id = table.Column<Guid>(nullable: true)
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
                });

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
                    Genero = table.Column<string>(maxLength: 20, nullable: true),
                    Estado_Civil = table.Column<string>(maxLength: 20, nullable: true),
                    Profissao = table.Column<string>(maxLength: 120, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "cad_clientes_pj",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Razao_Social = table.Column<string>(maxLength: 200, nullable: false),
                    Nome_Fantasia = table.Column<string>(maxLength: 200, nullable: false),
                    Cnpj = table.Column<string>(maxLength: 18, nullable: false),
                    Inscricao_Estadual = table.Column<string>(maxLength: 30, nullable: true),
                    Inscricao_Municipal = table.Column<string>(maxLength: 30, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "cad_clientes_financeiro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Limite_Credito = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Prazo_Pagamento = table.Column<int>(nullable: true),
                    Bloqueado = table.Column<bool>(nullable: false),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "cad_clientes_lgpd_consentimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Aceito = table.Column<bool>(nullable: false),
                    Data_Consentimento = table.Column<DateTime>(nullable: false),
                    Canal = table.Column<string>(maxLength: 80, nullable: false),
                    Observacao = table.Column<string>(maxLength: 240, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "cad_clientes_anexos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Nome_Arquivo = table.Column<string>(maxLength: 200, nullable: false),
                    Tipo_Conteudo = table.Column<string>(maxLength: 100, nullable: false),
                    Caminho_Arquivo = table.Column<string>(maxLength: 500, nullable: false),
                    Observacao = table.Column<string>(maxLength: 240, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "cad_clientes_contatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 40, nullable: false),
                    Valor = table.Column<string>(maxLength: 160, nullable: false),
                    Observacao = table.Column<string>(maxLength: 240, nullable: true),
                    Principal = table.Column<bool>(nullable: false, defaultValue: false)
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

            migrationBuilder.CreateTable(
                name: "cad_clientes_enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 40, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 160, nullable: false),
                    Numero = table.Column<string>(maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(maxLength: 120, nullable: true),
                    Bairro = table.Column<string>(maxLength: 120, nullable: false),
                    Cidade = table.Column<string>(maxLength: 120, nullable: false),
                    Estado = table.Column<string>(maxLength: 60, nullable: false),
                    Cep = table.Column<string>(maxLength: 12, nullable: false),
                    Pais = table.Column<string>(maxLength: 80, nullable: true),
                    Principal = table.Column<bool>(nullable: false, defaultValue: false)
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

            migrationBuilder.CreateTable(
                name: "cad_clientes_indicacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Indicador_Nome = table.Column<string>(maxLength: 160, nullable: false),
                    Indicador_Telefone = table.Column<string>(maxLength: 20, nullable: true),
                    Observacao = table.Column<string>(maxLength: 240, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "cad_veiculos_marcas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 120, nullable: false),
                    Pais = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_veiculos_marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cad_veiculos_modelos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Marca_Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 160, nullable: false),
                    Ano_Inicio = table.Column<int>(nullable: true),
                    Ano_Fim = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "cad_veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Cliente_Id = table.Column<Guid>(nullable: false),
                    Modelo_Id = table.Column<Guid>(nullable: false),
                    Placa = table.Column<string>(maxLength: 10, nullable: false),
                    Ano_Fabricacao = table.Column<int>(nullable: true),
                    Ano_Modelo = table.Column<int>(nullable: true),
                    Cor = table.Column<string>(maxLength: 40, nullable: true),
                    Renavam = table.Column<string>(maxLength: 20, nullable: true),
                    Chassi = table.Column<string>(maxLength: 40, nullable: true),
                    Combustivel = table.Column<string>(maxLength: 40, nullable: true),
                    Observacao = table.Column<string>(maxLength: 240, nullable: true)
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
                    Contato = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_fornecedores", x => x.Id);
                });

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
                name: "IX_cad_clientes_anexos_Cliente_Id_Nome_Arquivo",
                table: "cad_clientes_anexos",
                columns: new[] { "Cliente_Id", "Nome_Arquivo" },
                unique: true);

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
                name: "IX_cad_clientes_pf_Cpf",
                table: "cad_clientes_pf",
                column: "Cpf",
                unique: true);

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
                filter: "[Renavam] IS NOT NULL");

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
