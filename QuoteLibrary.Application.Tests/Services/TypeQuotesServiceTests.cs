using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Tests.Services
{
    public class TypeQuotesServiceTests
    {
        private readonly TypesQuotesService _typeQuotesService;

        private readonly Mock<ITypesQuotesRepository> _mockTypeQuotesRepository;
        private readonly Mock<ITokenService> _mockTokenService;

        public TypeQuotesServiceTests()
        {
            _mockTypeQuotesRepository = new Mock<ITypesQuotesRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _typeQuotesService = new TypesQuotesService(_mockTypeQuotesRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task GetTypeQuotesByIdAsync_ReturnTypeQuotesDto_WhenTypeQuotesExists()
        {
            //Arrange
            var typeQuotesId = 1;
            var typeQuotes = new TypesQuotes { 
                Id = typeQuotesId, 
                Name = "Pelicula", 
                CreationDate = new DateTime(2024, 10, 3), 
                ModificationDate = new DateTime(2024, 10, 6)
            };

            _mockTypeQuotesRepository.Setup(repo => repo.GetTypesQuotesByIdAsync(typeQuotesId)).ReturnsAsync(typeQuotes);
            
            //Act
            var result = await _typeQuotesService.GetTypesQuotesByIdAsync(typeQuotesId);


            //Assert
            Assert.NotNull(result);
            Assert.True(result.GetType() == typeof(TypesQuotesDto));
            Assert.Equal("Pelicula", result.Name);
            Assert.Equal(new DateTime(2024, 10, 3), result.CreationDate);

        }

        [Fact]
        public async Task GetAllTypesQuotesAsync_ReturnsListOfTypesQuotesDto()
        {
            //Arrange
            var typesQuotesList = new List<TypesQuotes>
            {
                new TypesQuotes { Id = 1, Name = "Quote 1", CreationDate = DateTime.Now },
                new TypesQuotes { Id = 2, Name = "Quote 2", CreationDate = DateTime.Now },
                new TypesQuotes { Id = 3, Name = "Quote 3", CreationDate = DateTime.Now }
            };

            _mockTypeQuotesRepository.Setup(repo => repo.GetAllTypesQuotesAsync()).ReturnsAsync(typesQuotesList);

            //Act
            var result = await _typeQuotesService.GetAllTypesQuotesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 3);
        }

        [Fact]
        public async Task CreateTypesQuotesAsync_AddNewTypesQuotes_ReturnsNewId()
        {
            //Arrange
            var typeQuotesName = "Quotes 5";

            _mockTypeQuotesRepository.Setup(repo => repo.CreateTypesQuotesAsync(It.IsAny<TypesQuotes>()))
                                        .ReturnsAsync(1);

            //Act
            var result = await _typeQuotesService.CreateTypesQuotesAsync(typeQuotesName);


            //Assert
            Assert.True(result == 1);          
        }

        [Fact]
        public async Task UpdateTypesQuotesAsync_UpdateTypesQuotes_ReturnBool()
        {
            //Arrange
            var typeQuotesId = 1;
            var typeQuotes = new TypesQuotes
            {
                Id = typeQuotesId,
                Name = "Pelicula",
                CreationDate = new DateTime(2024, 10, 3),
                ModificationDate = new DateTime(2024, 10, 6)
            };

            _mockTypeQuotesRepository.Setup(repo => repo.GetTypesQuotesByIdAsync(typeQuotesId)).ReturnsAsync(typeQuotes);

            _mockTypeQuotesRepository.Setup(repo => repo.UpdateTypesQuotesAsync(It.IsAny<TypesQuotes>())).ReturnsAsync(true);

            //Act
            var result = await _typeQuotesService.UpdateTypesQuotesAsync(1, "Quotes 7");

            //Assert
            Assert.True(result == true);
        }

        [Fact]
        public async Task DeleteTypesQuotesAsync_DeleteTypesQuotes_ReturnBool()
        {
            //Arrange
            var typeQuotesId = 1;

            _mockTypeQuotesRepository.Setup(repo => repo.DeleteTypesQuotesAsync(typeQuotesId)).ReturnsAsync(true);

            //Act
            var result = await _typeQuotesService.DeleteTypesQuotesAsync(typeQuotesId);

            //Assert
            Assert.True(result == true);

        }
    }
}
