using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Wager;

namespace OT.Assessment.App.Controllers.Wager
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagerController : ControllerBase
    {
        private readonly IWagerService _wagerService;
        public WagerController(IWagerService wagerService)
        {
            _wagerService = wagerService;
        }


        [HttpPost]
        [Route("player/casinowager")]
        public async Task<IActionResult> Create([FromBody] WagerDto dto)
        {
            var result = await _wagerService.CreateWager(dto);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Accepted(result);
        }
    }
}
