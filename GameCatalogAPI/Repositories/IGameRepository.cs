using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;

namespace GameCatalogAPI.Repositories
{
    public interface IGameRepository
    {
        Task<List<GameViewModel>> GetGames(int page, int qtd);
        Task<GameViewModel> GetGameById(Guid id);
        Task<GameViewModel> GetGameByName(string name, string producer);
        Task<GameViewModel> PostGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}
