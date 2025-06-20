using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Game;
using OT.Assessment.Application.Models.Request;

namespace OT.Assessment.App.Controllers.Game
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        [Route("/createcasinogame")]
        public async Task<IActionResult> CreateGame([FromBody] CreateCasinoGameRequestModel request)
        {
            //var result = await _casinoWagerService.CreateWagerAsync(request);
            if (request is null)
            {
                return BadRequest();
            }

            var response = await _gameService.CreateGame(new GameDto
            {
                GameCode = request.GameCode,
                Name = request.Name,
            });

            return Ok(response);
        }
        [HttpGet]
        [Route("/getcasinogames")]
        public async Task<IActionResult> GetGames()
        {

            var response = await _gameService.GetGames();

            return Ok(response);
        }
    }
}
