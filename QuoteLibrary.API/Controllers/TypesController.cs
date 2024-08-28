using Microsoft.AspNetCore.Mvc;
using QuoteLibrary.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuoteLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypesQuotesService _typesQuotesService;

        public TypesController(ITypesQuotesService typesQuotesService)
        {
            _typesQuotesService = typesQuotesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetAllTypesQuotes()
        {
            var types = await _typesQuotesService.GetAllTypesQuotesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetTypeQuotesById(int id)
        {
            var type = await _typesQuotesService.GetTypeQuotesByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }

        // POST api/<TypesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TypesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TypesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
