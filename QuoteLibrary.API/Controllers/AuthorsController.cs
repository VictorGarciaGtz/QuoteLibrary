﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs.Author;
using QuoteLibrary.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuoteLibrary.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService) { 
            _authorsService = authorsService; 
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAuthorDto>>> Get()
        {
            return Ok(await _authorsService.GetAllAuthorsAsync());
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDto>> Get(int id)
        {
            var author = await _authorsService.GetAuthorsByIdAsync(id);
            if(author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<ActionResult<AuthorDetailsDto>> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            if (string.IsNullOrEmpty(authorDto.Name))
            {
                return BadRequest("Name is required.");
            }

            var id = await _authorsService.CreateAuthorsAsync(authorDto);
            var createAuthor = await _authorsService.GetAuthorsByIdAsync(id);

            return CreatedAtAction(nameof(Get), new { id = createAuthor.Id }, createAuthor);
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
        {
            if (string.IsNullOrEmpty(authorDto.Name))
            {
                return BadRequest("Name is required.");
            }
            var result = await _authorsService.UpdateAuthorsAsync(id, authorDto);
            if(!result)
            {
                return NotFound();
            }

            return NoContent();

        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorsService.DeleteAuthorsAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
