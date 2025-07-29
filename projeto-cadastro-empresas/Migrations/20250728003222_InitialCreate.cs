using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroEmpresasApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    usu_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usu_nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usu_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usu_senha_hash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usu_id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_nome_empresarial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_nome_fantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_situacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_abertura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_natureza_juridica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_atividade_principal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usu_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.emp_id);
                    table.ForeignKey(
                        name: "FK_Empresas_Usuarios_usu_id",
                        column: x => x.usu_id,
                        principalTable: "Usuarios",
                        principalColumn: "usu_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_usu_id",
                table: "Empresas",
                column: "usu_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
