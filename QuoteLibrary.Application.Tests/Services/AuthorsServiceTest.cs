using Moq;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Tests.Services
{
    public class AuthorsServiceTest
    {
        private readonly AuthorsService _authorsService;

        private readonly Mock<IAuthorsRepository> _mockAuthorsRepository;
        private readonly Mock<ITokenService> _mockTokenService;

        public AuthorsServiceTest()
        {
            _mockAuthorsRepository = new Mock<IAuthorsRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _authorsService = new AuthorsService( _mockAuthorsRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task GetAuthorsByIdAsync_ReturnAuhtorsDto_WhenAuthotsExist()
        {
            //Arrange
            var authorId = 1;
            var author = new Authors
            {
                Id = authorId,
                Name = "Guillermo del toro",
                BirthDate = new DateTime(1964, 10, 9),
                CreationDate = DateTime.Now,
                IdNationality = 1
            };

            _mockAuthorsRepository.Setup(repo => repo.GetAuthorsByIdAsync(authorId)).ReturnsAsync(author);

            //Act
            var result = await _authorsService.GetAuthorsByIdAsync(1);

            //Asset
            Assert.NotNull(result);
            Assert.Equal("Guillermo del toro", result.Name);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_ReturnListOfAuthorsDto()
        {
            //Arrange
            var auhtors = new List<Authors>
            {
                new Authors { Id = 1, Name = "Guillermo del Toro", BirthDate = new DateTime(1964, 10, 9), IdNationality = 1, CreationDate = DateTime.Now },
                new Authors { Id = 2, Name = "Robert Greene", BirthDate = new DateTime(1959, 5, 14), IdNationality = 2, CreationDate = DateTime.Now },
                new Authors { Id = 2, Name = "Stephen King", BirthDate = new DateTime(1947, 9, 21), IdNationality = 2, CreationDate = DateTime.Now }
            };

            _mockAuthorsRepository.Setup(repo => repo.GetAllAuthorsAsync()).ReturnsAsync(auhtors);

            //Act
            var result = await _authorsService.GetAllAuthorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 3);
        }

        [Fact]
        public async Task CreateAuthorsAsync_ReturnNewId()
        {
            //Arrange 
            var authorId = 1;
            var authorDto = new AuthorsDto()
            {
                Id = 0,
                Name = "Gorge RR. Martin",
                BirthDate = new DateTime(1948, 9, 20),
                CreationDate = DateTime.Now,
                IdNationality = 2
            };

            _mockAuthorsRepository.Setup(repo => repo.CreateAuthorsAsync(It.IsAny<Authors>()))
                .ReturnsAsync(authorId);

            //Act
            var result = await _authorsService.CreateAuthorsAsync(authorDto);

            //Assert
            Assert.True(result == 1);
        }

        [Fact]
        public async Task UpdateAuthorsAsync_ReturnBool()
        {
            //Arrange
            var authorId = 1;
            var author = new Authors
            {
                Id = 1,
                Name = "Guillermo del Toro",
                BirthDate = new DateTime(1964, 10, 9),
                CreationDate = DateTime.Now,
                IdNationality = 1
            };

            var authorDtoUpdate = new AuthorsDto
            {
                Id = 1,
                Name = "Guillermo del Toro Gómez",
                BirthDate = new DateTime(1964, 10, 9),
                CreationDate = DateTime.Now,
                IdNationality = 1
            };

            _mockAuthorsRepository.Setup(repo => repo.GetAuthorsByIdAsync(authorId)).ReturnsAsync(author);

            _mockAuthorsRepository.Setup(repo => repo.UpdateAuthorsAsync(It.IsAny<Authors>())).ReturnsAsync(true);

            //Act
            var result = await _authorsService.UpdateAuthorsAsync(authorId, authorDtoUpdate);

            //Assert
            Assert.True(result == true);
        }

        [Fact]
        public async Task DeleteAuthorsAsync_ReturnBool()
        {
            //Arrange
            var authorsId = 1;

            _mockAuthorsRepository.Setup(repo => repo.DeleteAuthorsAsync(authorsId)).ReturnsAsync(true);

            //Act
            var result = await _authorsService.DeleteAuthorsAsync(authorsId);

            //Assert
            Assert.True(result == true);
        }
    }
}
