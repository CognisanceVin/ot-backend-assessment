using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Player;
using OT.Assessment.Application.Models.Request;
using OT.Assessment.Application.Models.Requests;
namespace OT.Assessment.App.Controllers.Player
{

    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("/getplayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetPlayers();
            return Ok(players);
        }

        [HttpGet]
        [Route("/getplayer")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            var player = await _playerService.GetPlayer(id);
            return Ok(player);
        }

        [HttpPost]
        [Route("/createplayer")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequestModel request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var result = await _playerService.CreatePlayer(new PlayerDto
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Username = request.UserName,
                CountryCode = request.CountryCode,
                EmailAddress = request.EmailAddress
            });

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
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
