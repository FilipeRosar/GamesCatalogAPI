using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;
using GameCatalogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int qtd = 5)
        {
            var result = await _gameService.GetGames(page, qtd);

            if (result.Count() == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGameById([FromRoute] Guid idGame)
        {
            var result = await _gameService.GetGameById(idGame);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var result = await _gameService.PostGame(gameInputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add game: {GameName}", gameInputModel.Name);
                return UnprocessableEntity("There is already a game with this name for this producer");
            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGame(idGame, game);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update game {idGame}", idGame);
                return NotFound("Game not found.");
            }
        }
        

        [HttpPatch("{idGame:guid}/price/{price:double.F2}")]
        public async Task<ActionResult> UpdateGame([FromRoute]Guid idGame,[FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGame(idGame, price);
                return Ok();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Failed to update price for game {idGame}", idGame);
                return NotFound("Game not found.");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.DeleteGame(idGame);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete game {idGame}", idGame);
                return NotFound("Game not found.");
            }
        }
    }
}
