using QuoteLibrary.Application.DTOs;
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
        Task<IEnumerable<QuotesDto>> GetAllQuotesAsync();
        Task<QuotesDto?> GetQuotesByIdAsync(int id);
        Task<int> CreateQuotesAsync(QuotesDto quoteDto);
        Task<bool> UpdateQuotesAsync(int id, QuotesDto quoteDto);
        Task<bool> DeleteQuotesAsync(int id);
    }
}
