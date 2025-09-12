using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;

namespace GameCatalogAPI.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGames(int page, int qtd);
        Task<GameViewModel> GetGameById(Guid id);
        Task<GameViewModel> PostGame(GameInputModel game);
        Task UpdateGame(Guid id, GameInputModel game);
        Task UpdateGame(Guid id, double price);
        Task DeleteGame(Guid id);
    }
}

