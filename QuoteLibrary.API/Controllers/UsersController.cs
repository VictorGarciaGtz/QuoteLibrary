using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs.User;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;

namespace QuoteLibrary.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto usersDto)
        {
            var id = await _usersService.CreateUserAsync(usersDto);

            return Ok();
        }

        [HttpPost("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ExistUserWithUsernameOrEmail([FromBody] CreateUserDto usersDto)
        {
            var existUser = await _usersService.ExistUserWithUsernameOrEmail(usersDto);

            if(existUser) { return  Unauthorized(); }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDetailsDto>> GetUserById(int id)
        {
            var userDto = await _usersService.GetUsersByIdAsync(id);

            if (userDto == null) return NotFound();

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto usersUpdateDto, int id)
        {
            var result = await _usersService.UpdateUsersAsync(id, usersUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _usersService.DeleteUsersAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
