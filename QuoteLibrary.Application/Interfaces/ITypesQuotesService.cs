using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface ITypesQuotesService
    {
        Task<IEnumerable<TypesQuotes>> GetAllTypesQuotesAsync();
        Task<TypesQuotes?> GetTypeQuotesByIdAsync(int id);
    }
}
