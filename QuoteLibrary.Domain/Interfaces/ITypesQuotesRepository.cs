using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface ITypesQuotesRepository
    {
        Task<IEnumerable<TypesQuotes>> GetAllTypesQuotesAsync();
        Task<TypesQuotes?> GetTypesQuotesByIdAsync(int id);
        Task<int> CreateTypesQuotesAsync(TypesQuotes type);
        Task<bool> UpdateTypesQuotesAsync(TypesQuotes type);
        Task<bool> DeleteTypesQuotesAsync(int id);
    }
}
