using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;

namespace QuoteLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public RegisterUsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var registed = await _userAppService.RegisterUserAsync(registerUserDto.Username, registerUserDto.Password);

            if (!registed) { return Unauthorized(); }

            return Ok();
        }
    }
}
