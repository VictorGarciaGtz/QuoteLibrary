using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure.Authentication;

namespace QuoteLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public AuthController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var token = await _userAppService.AuthenticateAsync(userDto.Username, userDto.Password);

            if (token == null) { return Unauthorized(); }

            return Ok(new { token });
        }
       
    }
}
