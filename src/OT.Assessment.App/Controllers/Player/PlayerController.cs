using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.Requests;
namespace OT.Assessment.App.Controllers.Player
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {

        private readonly IGameService _gameService;

        public PlayerController(IGameService gameService)
        {
            _gameService = gameService;
        }

        //GET api/player/
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            //var result = await _casinoWagerService.CreateWagerAsync(request);
            return Ok();
        }

        //POST api/player
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreateCasinoWagerRequestModel request)
        {
            //var result = await _casinoWagerService.CreateWagerAsync(request);
            return Ok();
        }

        //POST api/player/casinowager
        [HttpPost]
        [Route("/casinowager")]
        public async Task<IActionResult> CreateCasinoWager([FromBody] CreateCasinoWagerRequestModel request)
        {
            //var result = await _casinoWagerService.CreateWagerAsync(request);
            return Ok();
        }

        //GET api/player/{playerId}/wagers
        [HttpGet]
        [Route("/{playerId}/wager")]
        public async Task<IActionResult> GetPlayerWagers([FromRoute] string playerId)
        {
            //var result = await _casinoWagerService.CreateWagerAsync(request);
            return Ok();
        }

        //GET api/player/topSpenders?count=10
        [HttpGet]
        [Route("/topSpenders")]
        public async Task<IActionResult> GetTopSpenders([FromQuery] string count)
        {
            List<CreateCasinoWagerRequestModel> result = new List<CreateCasinoWagerRequestModel>();
            return Ok(result);
        }
    }
}
