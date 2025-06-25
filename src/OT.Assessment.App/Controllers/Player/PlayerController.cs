using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Player;
using OT.Assessment.Application.Models.Request;

namespace OT.Assessment.App.Controllers.Player
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _playerService;
        private readonly IWagerService _wagerService;

        public PlayerController(IPlayerService playerService, IWagerService wagerService)
        {
            _playerService = playerService;
            _wagerService = wagerService;
        }

        [HttpGet("getplayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetPlayers();
            return Ok(players);
        }

        [HttpGet("getplayer/{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId)
        {
            var player = await _playerService.GetPlayer(playerId);
            return Ok(player);
        }

        [HttpPost("createplayer")]
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

        [HttpGet("{playerId}/casino")]
        public async Task<IActionResult> GetPlayerWagers([FromRoute] Guid playerId, [FromQuery] int pageSize = 10, [FromQuery] int page = 1)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Invalid page number and page size.");


            var result = await _wagerService.GetPlayerWagers(playerId, page, pageSize);

            return Ok(result);
        }

        [HttpGet("topSpenders")]
        public async Task<IActionResult> GetTopSpenders([FromQuery] int count)
        {
            if (count <= 0)
                return BadRequest("Count must be greater than 0.");

            var result = await _wagerService.GetTopSpenders(count);
            return Ok(result);
        }
    }
}
