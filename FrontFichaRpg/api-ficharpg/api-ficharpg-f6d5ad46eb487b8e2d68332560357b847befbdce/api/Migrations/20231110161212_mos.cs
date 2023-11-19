using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class mos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_criacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    deletado = table.Column<bool>(type: "INTEGER", nullable: true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "fichas_rpg",
                columns: table => new
                {
                    id_ficha_rpg = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    antecedencia = table.Column<string>(type: "TEXT", nullable: false),
                    nome_do_jogador = table.Column<string>(type: "TEXT", nullable: false),
                    raca = table.Column<string>(type: "TEXT", nullable: false),
                    alinhamento = table.Column<string>(type: "TEXT", nullable: false),
                    pontos_de_experiencia = table.Column<int>(type: "INTEGER", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_atualizado = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deletado = table.Column<bool>(type: "INTEGER", nullable: true),
                    id_usuario = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fichas_rpg", x => x.id_ficha_rpg);
                    table.ForeignKey(
                        name: "fk_fichas_rpg_usuarios_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habilidades",
                columns: table => new
                {
                    id_habilidade = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    pontos = table.Column<int>(type: "INTEGER", nullable: false),
                    id_ficha_rpg = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_habilidades", x => x.id_habilidade);
                    table.ForeignKey(
                        name: "fk_habilidades_fichas_rpg_ficha_rpg_id_ficha_rpg",
                        column: x => x.id_ficha_rpg,
                        principalTable: "fichas_rpg",
                        principalColumn: "id_ficha_rpg",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fichas_rpg_id_usuario",
                table: "fichas_rpg",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_habilidades_id_ficha_rpg",
                table: "habilidades",
                column: "id_ficha_rpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "habilidades");

            migrationBuilder.DropTable(
                name: "fichas_rpg");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
