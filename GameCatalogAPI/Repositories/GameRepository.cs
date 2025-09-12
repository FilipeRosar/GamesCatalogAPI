using GameCatalogAPI.Entities;
using GameCatalogAPI.Models.ViewModel;

namespace GameCatalogAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {
                Guid.Parse("a3f9c9c4-1c76-4a3e-872e-2e42dbe0a4bb"),
                new Game
                {
                    Id = Guid.Parse("a3f9c9c4-1c76-4a3e-872e-2e42dbe0a4bb"),
                    Name = "The Legend of Zelda",
                    Producer = "Nintendo",
                    Description = "Action-adventure game",
                    Price = 59.99
                }
            },
            {
                Guid.Parse("b2d8f2b3-5f0d-4996-b0c9-7369cf95c3d2"),
                new Game
                {
                    Id = Guid.Parse("b2d8f2b3-5f0d-4996-b0c9-7369cf95c3d2"),
                    Name = "Minecraft",
                    Producer = "Mojang",
                    Description = "Sandbox survival game",
                    Price = 29.99
                }
            },
            {
                Guid.Parse("c6e1d1f4-ecf9-4f87-88e1-4fcbbe8d7de3"),
                new Game
                {
                    Id = Guid.Parse("c6e1d1f4-ecf9-4f87-88e1-4fcbbe8d7de3"),
                    Name = "God of War",
                    Producer = "Santa Monica Studio",
                    Description = "Mythology-based action game",
                    Price = 69.99
                }
            },
            {
                Guid.Parse("d9a67b6d-0c41-4f2d-bec7-f346b589b9f5"),
                new Game
                {
                    Id = Guid.Parse("d9a67b6d-0c41-4f2d-bec7-f346b589b9f5"),
                    Name = "Elden Ring",
                    Producer = "FromSoftware",
                    Description = "Open-world RPG",
                    Price = 79.99
                }
            },
            {
                Guid.Parse("e4c1b7a1-f642-47f3-9480-8126ef0c38aa"),
                new Game
                {
                    Id = Guid.Parse("e4c1b7a1-f642-47f3-9480-8126ef0c38aa"),
                    Name = "FIFA 24",
                    Producer = "EA Sports",
                    Description = "Soccer simulation",
                    Price = 49.99
                }
            }
        };

        public Task<List<Game>> GetGames(int page, int qtd)
        {
            var gameList = games
                .Values
                .Skip((page - 1) * qtd)
                .Take(qtd)
                .ToList();

            return Task.FromResult(gameList);
        }

        public Task<Game> GetGameById(Guid id)
        {
            games.TryGetValue(id, out var game);
            return Task.FromResult(game);
        }

        public Task<Game> GetGameByName(string name, string producer)
        {
            var game = games.Values.FirstOrDefault(g =>
                g.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                g.Producer.Equals(producer, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(game);
        }

        public Task<Game> PostGame(Game game)
        {
            games.Add(game.Id, game);
            return Task.FromResult(game);
        }

        public Task UpdateGame(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task DeleteGame(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Nenhum recurso para liberar neste caso.
        }
    }
}
