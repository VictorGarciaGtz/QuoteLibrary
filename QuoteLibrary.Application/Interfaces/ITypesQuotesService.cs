using QuoteLibrary.Application.DTOs.TypesQuotes;

namespace QuoteLibrary.Application.Interfaces
{
    public interface ITypesQuotesService
    {
        Task<IEnumerable<GetTypesQuotesDto>> GetAllTypesQuotesAsync();
        Task<TypesQuotesDetailsDto?> GetTypesQuotesByIdAsync(int id);

        Task<int> CreateTypesQuotesAsync(string name);
        Task<bool> UpdateTypesQuotesAsync(int id, string name);
        Task<bool> DeleteTypesQuotesAsync(int id);
    }
}
