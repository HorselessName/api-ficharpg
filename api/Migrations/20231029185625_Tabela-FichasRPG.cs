using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class TabelaFichasRPG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "fichas_rpg",
                columns: table => new
                {
                    id_ficha_rpg = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nível = table.Column<int>(type: "INTEGER", nullable: false),
                    antecedente = table.Column<string>(type: "TEXT", nullable: false),
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
                    _ = table.PrimaryKey("pk_fichas_rpg", x => x.id_ficha_rpg);
                    _ = table.ForeignKey(
                        name: "fk_fichas_rpg_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "ix_fichas_rpg_id_usuario",
                table: "fichas_rpg",
                column: "id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "fichas_rpg");
        }
    }
}
