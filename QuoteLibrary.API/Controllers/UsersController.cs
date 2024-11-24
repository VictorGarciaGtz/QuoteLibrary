using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UsersInsertDto usersDto)
        {
            var id = await _usersService.CreateUserAsync(usersDto);

            var userDto = _usersService.GetUsersByIdAsync(id);

            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPost("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ExistUserWithUsernameOrEmail([FromBody] UsersInsertDto usersDto)
        {
            var existUser = await _usersService.ExistUserWithUsernameOrEmail(usersDto);

            if(existUser) { return  Unauthorized(); }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UsersDto>> GetUserById(int id)
        {
            var userDto = await _usersService.GetUsersByIdAsync(id);

            if (userDto == null) return NotFound();

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateDto usersUpdateDto, int id)
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
