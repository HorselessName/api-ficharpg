using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // Classe Principal entre nossa aplicação e o banco de dados.
    public class AppDataContext : DbContext
    {
        // Definir o tipo de banco para persistencia com Options para diversos bancos.
        public AppDataContext(DbContextOptions<AppDataContext> options) :
            // Base: Mesmo que Super() do Java, herda da classe DbContextOptions.
            base(options) { }

        // Definição de Classes que serão nossas tabelas
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
