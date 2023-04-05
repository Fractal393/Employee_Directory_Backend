using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectoryAPI.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }



        [HttpGet("Tokens")]
        public async Task<ActionResult<Token>> GetAll()
        {
            var result = await _tokenService.GetAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Tokens")]
        public async Task<IActionResult> Add([FromBody] Token token)
        {
            var result = await _tokenService.AddAsync(token);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

    }
}
