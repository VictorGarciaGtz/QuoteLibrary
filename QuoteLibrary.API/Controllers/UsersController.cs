using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;

namespace QuoteLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UsersDto usersDto)
        {
            var id = await _usersService.CreateUserAsync(usersDto);

            return Ok(id);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ExistUserWithUsernameOrEmail([FromBody] UsersDto usersDto)
        {
            var existUser = await _usersService.ExistUserWithUsernameOrEmail(usersDto);

            if(existUser) { return  Unauthorized(); }

            return Ok();
        }
    }
}
