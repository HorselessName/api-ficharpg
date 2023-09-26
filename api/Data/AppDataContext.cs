using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // Classe Principal entre nossa aplicação e o banco de dados.
    public class AppDataContext : DbContext
    {
        // Definir o tipo de banco para persistência com Options para diversos bancos.
        public AppDataContext(DbContextOptions<AppDataContext> options) :
            // Base: Mesmo que Super() do Java, herda da classe DbContextOptions.
            base(options)
        { }

        // Definição de Classes que serão nossas tabelas
        public DbSet<Usuario> Usuarios { get; set; }

        // Definição de Classes que serão nossas tabelas
        public DbSet<FichaRpg> FichasRpg { get; set; }

        // Definição de Classes que serão nossas tabelas
        public DbSet<Habilidade> Habilidades { get; set; }

        // Configuração de relacionamentos no método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento "um para muitos" entre Usuario e FichasRpg
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.FichasRpg)
                .WithOne(fr => fr.Usuario)
                .HasForeignKey(fr => fr.UsuarioId);

            // Configuração do relacionamento "um para muitos" entre FichaRpg e Habilidades
            modelBuilder.Entity<FichaRpg>()
                .HasMany(fr => fr.Habilidades)
                .WithOne(h => h.FichaRpg)
                .HasForeignKey(h => h.FichaRpgId);
        }
    }
}
