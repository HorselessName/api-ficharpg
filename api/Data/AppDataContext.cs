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
            // ##### Definição das Tabelas e Relacionamentos #####
            // Chaves Primários
            modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.IdUsuario);
            modelBuilder.Entity<Habilidade>().HasKey(habilidade => habilidade.IdHabilidade);

            // FichaRPG - Alterações começam aqui

            // - Tem um relacionamento com o objeto Usuario de 1..N
            // - Não tem navigation properties para evitar dependências
            // - Possui chave estrangeira nos relacionamentos
            // - Ficha RPG depende de um usuario existir e habilidade existir
            modelBuilder.Entity<FichaRpg>(ficha => {
                ficha.HasKey(fichaid => fichaid.IdFichaRpg);  // Adicionado chave primária para FichaRpg

                // ##### Relacionamento Usuario ##### - Alterado para estabelecer corretamente o relacionamento
                ficha.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(usuarioid => usuarioid.IdUsuario)
                .IsRequired();

                // ##### Relacionamento Habilidades ##### - Alterado para estabelecer corretamente o relacionamento
                
                
            });

            
        }
    }
}
