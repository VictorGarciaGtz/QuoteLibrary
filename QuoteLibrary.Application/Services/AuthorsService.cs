using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<int> CreateAuthorsAsync(AuthorsDto authorDto)
        {
            var author = new Authors
            {
                Name = authorDto.Name,
                BirthDate = authorDto.BirthDate,
                IdNationality = authorDto.IdNationality,
                PhotoUrl = authorDto.PhotoUrl,
                CreationDate = DateTime.Now
            };

            return await _authorsRepository.CreateAuthorsAsync(author);

        }

        public async Task<bool> DeleteAuthorsAsync(int id)
        {
            return await _authorsRepository.DeleteAuthorsAsync(id);
        }

        public async Task<IEnumerable<AuthorsDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorsRepository.GetAllAuthorsAsync();

            return authors.Select(x => new AuthorsDto { 
                Id = x.Id,
                Name = x.Name, 
                BirthDate = x.BirthDate, 
                IdNationality = x.IdNationality, 
                PhotoUrl = x.PhotoUrl, 
                CreationDate = x.CreationDate,
                ModificationDate = x.ModificationDate
            });
        }

        public async Task<AuthorsDto?> GetAuthorsByIdAsync(int id)
        {
            var author = await _authorsRepository.GetAuthorsByIdAsync(id);
            return author == null ? null : new AuthorsDto { 
                Id = author.Id,
                Name = author.Name, 
                BirthDate = author.BirthDate, 
                CreationDate = author.CreationDate, 
                ModificationDate = author.ModificationDate,
                IdNationality = author.IdNationality
            };
        }

        public async Task<bool> UpdateAuthorsAsync(int id, AuthorsDto authorDto)
        {
            var author = await _authorsRepository.GetAuthorsByIdAsync(authorDto.Id ?? 0);
            if (author == null)
            {
                return false;
            }
            author.Name = authorDto.Name;
            author.BirthDate = authorDto.BirthDate;
            author.CreationDate = DateTime.Now;
            author.PhotoUrl = authorDto.PhotoUrl;
            author.ModificationDate = authorDto.ModificationDate;

            return await _authorsRepository.UpdateAuthorsAsync(author);

        }
    }
}
