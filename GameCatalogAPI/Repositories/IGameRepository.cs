using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public interface IGameRepository  : IDisposable
    {
        Task<List<Game>> GetGames(int page, int qtd);
        Task<Game?> GetGameById(Guid id);
        Task<List<Game>> GetGamesByName(string name, string producer);
        Task InsertGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}
