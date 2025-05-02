using GameCatalogAPI.Models.InputModel;
using GameCatalogAPI.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get()
        {
            return Ok();
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Get(Guid idGame)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame(GameInputModel game)
        {
            return Ok();
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame(Guid idGame, GameInputModel game)
        {
            return Ok();
        }

        [HttpPatch("{idGame:guid}/price{price:double}")]
        public async Task<ActionResult> UpdateGame(Guid idGame, double price)
        {
            return Ok();
        }
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame(Guid idGame)
        {
            return BadRequest();
        }
    }
}
