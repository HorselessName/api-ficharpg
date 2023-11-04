using api.Models;

namespace api.Services
{
    // Serviço de "Carregamento de Dados" das Habilidades.
    public class HabilidadesService
    {
        // Usamos este serviço para adicionar essas habilidades dentro do nosso banco de dados.
        public static List<Habilidade> GetCategories()
        {
            // Criar uma lista de habilidades.
            List<Habilidade> habilidades = new List<Habilidade>();

            // Lista de Habilidades da Ficha de RPG.
            habilidades.Add(new Habilidade { Nome = "Destreza", Pontos = 0 });
            habilidades.Add(new Habilidade { Nome = "Força", Pontos = 0 });
            habilidades.Add(new Habilidade { Nome = "Sabedoria", Pontos = 0 });
            habilidades.Add(new Habilidade { Nome = "Inteligencia", Pontos = 0 });
            habilidades.Add(new Habilidade { Nome = "Constituição", Pontos = 0 });

            // Retornar a lista com as nossas habilidades.
            return habilidades;
        }
    }
}
