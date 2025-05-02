using GameCatalogAPI.Entities;
using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;
using GameCatalogAPI.Repositories;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
        public async Task<GameViewModel> GetById(Guid id)
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
                throw new Exception();
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
            return new GameViewModel
            {
                Id = gamePost.Id,
                Name = gamePost.Name,
                Producer = gamePost.Producer,
                Description = gamePost.Description,
                Price = gamePost.Price
            };

        }
    }
}
