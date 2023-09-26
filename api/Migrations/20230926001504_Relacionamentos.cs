using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class Relacionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizado = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "FichasRpg",
                columns: table => new
                {
                    FichaRpgId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nível = table.Column<int>(type: "INTEGER", nullable: false),
                    Antecedência = table.Column<string>(type: "TEXT", nullable: false),
                    NomeDoJogador = table.Column<string>(type: "TEXT", nullable: false),
                    Raça = table.Column<string>(type: "TEXT", nullable: false),
                    Alinhamento = table.Column<string>(type: "TEXT", nullable: false),
                    PontosDeExperiência = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAtualizado = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deletado = table.Column<bool>(type: "INTEGER", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichasRpg", x => x.FichaRpgId);
                    table.ForeignKey(
                        name: "FK_FichasRpg_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    HabilidadeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Pontos = table.Column<int>(type: "INTEGER", nullable: false),
                    FichaRpgId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.HabilidadeId);
                    table.ForeignKey(
                        name: "FK_Habilidades_FichasRpg_FichaRpgId",
                        column: x => x.FichaRpgId,
                        principalTable: "FichasRpg",
                        principalColumn: "FichaRpgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FichasRpg_UsuarioId",
                table: "FichasRpg",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Habilidades_FichaRpgId",
                table: "Habilidades",
                column: "FichaRpgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "FichasRpg");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
