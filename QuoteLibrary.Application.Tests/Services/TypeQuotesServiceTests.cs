using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Services;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Tests.Services
{
    public class TypeQuotesServiceTests
    {
        private readonly TypesQuotesService _typeQuotesService;

        private readonly Mock<ITypesQuotesRepository> _mockTypeQuotesRepository;

        public TypeQuotesServiceTests()
        {
            _mockTypeQuotesRepository = new Mock<ITypesQuotesRepository>();
            _typeQuotesService = new TypesQuotesService(_mockTypeQuotesRepository.Object);
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

    }
}
