using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // Classe Principal entre nossa aplicação e o banco de dados.
    public class AppDataContext : DbContext
    {
        // Objeto para Definir o Tipo de Banco
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        // Definição de Classes / Tabelas do BD
        public DbSet<UsuarioModel>? Usuarios { get; set; }
        public DbSet<FichaRpgModel> FichasRpg { get; set; }
    }
}