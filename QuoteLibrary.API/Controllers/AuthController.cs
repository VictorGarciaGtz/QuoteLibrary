using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure.Authentication;

namespace QuoteLibrary.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUserService _userAppService;

        public AuthController(IAuthUserService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var token = await _userAppService.AuthenticateAsync(userDto.Username, userDto.Password);

            if (token == null) { return Unauthorized(); }

            return Ok(new { token });
        }
       
    }
}
