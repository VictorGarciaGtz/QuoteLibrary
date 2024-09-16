using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface IQuotesRepository
    {
        Task<IEnumerable<Quotes>> GetAllQuotesAsync();
        Task<Quotes?> GetQuotesByIdAsync(int id);
        Task<int> CreateQuotesAsync(Quotes quote);
        Task<bool> UpdateQuotesAsync(Quotes quote);
        Task<bool> DeleteQuotesAsync(int id);
    }
}
