using GameCatalogAPI.Entities;
using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;
using GameCatalogAPI.Repositories;
using Microsoft.Extensions.Logging;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly Logger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = (Logger<GameService>?)logger;
        }

        public async Task<List<GameViewModel>> GetGames(int page, int qtd)
        {
            var games = await _gameRepository.GetGames(page, qtd);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Description = game.Description,
                Price = game.Price
            }).ToList();
        }
        public async Task<GameViewModel> GetGameById(Guid id)
        {
            var game = await _gameRepository.GetGameById(id);

            if (game == null)
            {
                return null;
            }
            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Description = game.Description,
                Price = game.Price
            };
        }
        public async Task<GameViewModel> PostGame(GameInputModel game)
        {
            var entityGame = await _gameRepository.GetGameByName(game.Name, game.Producer);

            if (entityGame != null)
            {
                _logger.LogWarning("Attempted to create duplicate game: {Name}, {Producer}", game.Name, game.Producer);
                throw new ArgumentException($"Game '{game.Name}' by '{game.Producer}' already exists.");
            }
            var gamePost = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Description = game.Description,
                Price = game.Price
            };
            await _gameRepository.PostGame(gamePost);
            _logger.LogInformation($"Created game: {0}, ID: {1}", gamePost.Name, gamePost.Id);

            return new GameViewModel
            {
                Id = gamePost.Id,
                Name = gamePost.Name,
                Producer = gamePost.Producer,
                Description = gamePost.Description,
                Price = gamePost.Price
            };

        }
        public async Task UpdateGame(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.GetGameById(id);

            if (entityGame == null)
            {
                _logger.LogWarning("Game with ID {Id} not found for update.", id);
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }

            entityGame.Name = game.Name;
            entityGame.Producer = game.Producer;
            entityGame.Description = game.Description;
            entityGame.Price = game.Price;

            await _gameRepository.UpdateGame(entityGame);
            _logger.LogInformation("Updated game ID {Id}: Name='{Name}', Producer='{Producer}', Price={Price}.",
            id, game.Name, game.Producer, game.Price);


        }
        public async Task UpdateGame(Guid id, double price)
        {
            var entityGame = await _gameRepository.GetGameById(id);

            if (entityGame == null)
            {
                _logger.LogWarning("Game with ID {Id} not found for price update.", id);
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }
            entityGame.Price = price;

            await _gameRepository.UpdateGame(entityGame);
            _logger.LogInformation($"Updated price for game ID {id} to {price}.", id, price);


        }
        public async Task DeleteGame(Guid id)
        {
            var game = _gameRepository.GetGameById(id);

            if (game == null)
            {
                _logger.LogWarning($"Attemted to delete non-existing game with ID: {id}", id);
                throw new KeyNotFoundException($"Game with ID {id} not found");
            }
            await _gameRepository.DeleteGame(id);
            _logger.LogInformation($"Removed {game.Id}");
        }
        public void Dispose()
        {
            _gameRepository?.Dispose();
            _logger.LogInformation("GameService disposed.");
        }

        
    }
}
