using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Application.Interfaces.Services;
using OT.Assessment.Application.Models.DTOs.Account;

namespace OT.Assessment.App.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositToAccountDto depositDto)
        {
            var result = await _accountService.DepositToPlayerAccount(depositDto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { Message = "Deposit successful." });
        }
    }
}
