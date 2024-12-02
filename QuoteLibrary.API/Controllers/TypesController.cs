using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuoteLibrary.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class TypesController : ControllerBase
    {
        private readonly ITypesQuotesService _typesQuotesService;

        public TypesController(ITypesQuotesService typesQuotesService)
        {
            _typesQuotesService = typesQuotesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypesQuotesDto>>> GetAllTypesQuotes()
        {
            var types = await _typesQuotesService.GetAllTypesQuotesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypesQuotesDto>> GetTypesQuotesById(int id)
        {
            var typeDto = await _typesQuotesService.GetTypesQuotesByIdAsync(id);
            if (typeDto == null)
            {
                return NotFound();
            }
            return Ok(typeDto);
        }

        // POST: api/Types
        [HttpPost]
        public async Task<ActionResult<TypesQuotesDto>> CreateType([FromBody] TypesQuotesDto typesQuotesDto)
        {
            if (string.IsNullOrWhiteSpace(typesQuotesDto.Name))
            {
                return BadRequest("Name is required.");
            }

            var id = await _typesQuotesService.CreateTypesQuotesAsync(typesQuotesDto.Name);
            var createdTypeDto = await _typesQuotesService.GetTypesQuotesByIdAsync(id);

            return CreatedAtAction(nameof(GetTypesQuotesById), new { id = createdTypeDto.Id }, createdTypeDto);
        }

        // PUT: api/Types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(int id, [FromBody] TypesQuotesDto typesQuotesDto)
        {
            if (string.IsNullOrWhiteSpace(typesQuotesDto.Name))
            {
                return BadRequest("Name is required.");
            }

            var result = await _typesQuotesService.UpdateTypesQuotesAsync(id, typesQuotesDto.Name);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var result = await _typesQuotesService.DeleteTypesQuotesAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
