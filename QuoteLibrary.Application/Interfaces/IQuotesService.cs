using QuoteLibrary.Application.DTOs.Quote;
using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IQuotesService
    {
        Task<IEnumerable<GetQuoteDto>> GetAllQuotesAsync();
        Task<QuoteDetailsDto?> GetQuotesByIdAsync(int id);
        Task<int> CreateQuotesAsync(CreateQuoteDto quoteDto);
        Task<bool> UpdateQuotesAsync(int id, UpdateQuoteDto quoteDto);
        Task<bool> DeleteQuotesAsync(int id);
    }
}
