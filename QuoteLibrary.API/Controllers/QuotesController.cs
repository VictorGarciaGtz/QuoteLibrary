using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuoteLibrary.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;

        public QuotesController(IQuotesService quotesService) {
            _quotesService = quotesService;
        }

        // GET: api/<QuoteLibraryController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<QuotesDto>>> GetAllQuotes()
        {
            return Ok(await _quotesService.GetAllQuotesAsync());
        }

        // GET api/<QuoteLibraryController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<QuotesDto>> GetById(int id)
        {
            var quoteDto = await _quotesService.GetQuotesByIdAsync(id);

            if(quoteDto == null)
            {
                return NotFound();
            }

            return Ok(quoteDto);
        }

        // POST api/<QuoteLibraryController>
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post([FromBody] QuotesDto quotesDto)
        {
            var id = await _quotesService.CreateQuotesAsync(quotesDto);
            var quote = await _quotesService.GetQuotesByIdAsync(id);

            return CreatedAtAction(nameof(GetById), new { id = quote.Id }, quote);
        }

        // PUT api/<QuoteLibraryController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Put(int id, [FromBody] QuotesDto quotesDto)
        {
            var result = await _quotesService.UpdateQuotesAsync(id, quotesDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<QuoteLibraryController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _quotesService.DeleteQuotesAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
