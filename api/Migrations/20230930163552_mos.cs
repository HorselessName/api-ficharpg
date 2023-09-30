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
                    nível = table.Column<int>(type: "INTEGER", nullable: false),
                    antecedência = table.Column<string>(type: "TEXT", nullable: false),
                    nome_do_jogador = table.Column<string>(type: "TEXT", nullable: false),
                    raça = table.Column<string>(type: "TEXT", nullable: false),
                    alinhamento = table.Column<string>(type: "TEXT", nullable: false),
                    pontos_de_experiência = table.Column<int>(type: "INTEGER", nullable: false),
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
                    id_habilidade = table.Column<int>(type: "INTEGER", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    pontos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_habilidades", x => x.id_habilidade);
                    table.ForeignKey(
                        name: "fk_habilidades_fichas_rpg_ficha_rpg_temp_id",
                        column: x => x.id_habilidade,
                        principalTable: "fichas_rpg",
                        principalColumn: "id_ficha_rpg",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fichas_rpg_id_usuario",
                table: "fichas_rpg",
                column: "id_usuario");
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
