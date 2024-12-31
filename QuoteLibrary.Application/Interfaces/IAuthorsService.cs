using QuoteLibrary.Application.DTOs.Author;
using QuoteLibrary.Domain.Entities;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDetailsDto?> GetAuthorsByIdAsync(int id);
        Task<int> CreateAuthorsAsync(CreateAuthorDto authorDto);
        Task<bool> UpdateAuthorsAsync(int id, UpdateAuthorDto authorDto);
        Task<bool> DeleteAuthorsAsync(int id);
    }
}
