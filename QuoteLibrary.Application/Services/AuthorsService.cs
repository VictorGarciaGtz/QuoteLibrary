using QuoteLibrary.Application.DTOs.Author;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly ITokenService _tokenService;

        public AuthorsService(IAuthorsRepository authorsRepository, ITokenService tokenService)
        {
            _authorsRepository = authorsRepository;
            _tokenService = tokenService;
        }

        public async Task<int> CreateAuthorsAsync(CreateAuthorDto authorDto)
        {
            var userId = _tokenService.GetUserId();
            var author = new Authors
            {
                Name = authorDto.Name,
                BirthDate = authorDto.BirthDate,
                IdNationality = authorDto.IdNationality,
                PhotoUrl = authorDto.PhotoUrl,
                CreationDate = DateTime.Now,
                UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId),
            };

            return await _authorsRepository.CreateAuthorsAsync(author);

        }

        public async Task<bool> DeleteAuthorsAsync(int id)
        {
            return await _authorsRepository.DeleteAuthorsAsync(id);
        }

        public async Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorsRepository.GetAllAuthorsAsync();

            return authors.Select(x => new GetAuthorDto { 
                Id = x.Id ?? 0,
                Name = x.Name, 
                BirthDate = x.BirthDate, 
                IdNationality = x.IdNationality, 
                PhotoUrl = x.PhotoUrl
            });
        }

        public async Task<AuthorDetailsDto?> GetAuthorsByIdAsync(int id)
        {
            var author = await _authorsRepository.GetAuthorsByIdAsync(id);
            return author == null ? null : new AuthorDetailsDto { 
                Id = author.Id ?? 0,
                Name = author.Name, 
                BirthDate = author.BirthDate, 
                CreationDate = author.CreationDate, 
                ModificationDate = author.ModificationDate,
                IdNationality = author.IdNationality,
                PhotoUrl = author.PhotoUrl
            };
        }

        public async Task<bool> UpdateAuthorsAsync(int id, UpdateAuthorDto authorDto)
        {
            var author = await _authorsRepository.GetAuthorsByIdAsync(id == 0 ? authorDto.Id : id);
            if (author == null)
            {
                return false;
            }
            var userId = _tokenService.GetUserId();

            author.Name = authorDto.Name;
            author.BirthDate = authorDto.BirthDate;
            author.CreationDate = DateTime.Now;
            author.PhotoUrl = authorDto.PhotoUrl;
            author.UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId);

            return await _authorsRepository.UpdateAuthorsAsync(author);

        }
    }
}
