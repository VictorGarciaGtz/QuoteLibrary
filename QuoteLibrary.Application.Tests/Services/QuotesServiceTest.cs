using Moq;
using QuoteLibrary.Application.DTOs;
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
    public class QuotesServiceTest
    {
        private readonly QuotesService _quotesService;

        private readonly Mock<IQuotesRepository> _mockQuotesRepository;

        public QuotesServiceTest()
        {
            _mockQuotesRepository = new Mock<IQuotesRepository>();
            _quotesService = new QuotesService(_mockQuotesRepository.Object);
        }

        [Fact]
        public async Task GetQuotesByIdAsync_ReturnQuotesDtoExists()
        {
            //Arrange 
            var quotesId = 1;
            var quotes = new Quotes
            {
                Id = 1,
                Text = "Piensa 2 veces programa 1.",
                AuthorId = 1,
                TypeId = 1,
                CreationDate = DateTime.Now
            };

            _mockQuotesRepository.Setup(repo => repo.GetQuotesByIdAsync(quotesId))
                .ReturnsAsync(quotes);

            //Act
            var result = await _quotesService.GetQuotesByIdAsync(quotesId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Piensa 2 veces programa 1.", result.Text);
        }

        [Fact]
        public async Task GetAllQuotesAsync_ReturnListOfQuotesDto()
        {
            //Arrange
            var quotes = new List<Quotes> { 
                new Quotes { Id = 1, Text = "Quotes 1", AuthorId = 1, CreationDate = DateTime.Now, TypeId = 1 },
                new Quotes { Id = 2, Text = "Quotes 2", AuthorId = 2, CreationDate = DateTime.Now, TypeId = 1 },
                new Quotes { Id = 3, Text = "Quotes 3", AuthorId = 3, CreationDate = DateTime.Now, TypeId = 1 }
            };

            _mockQuotesRepository.Setup(repo => repo.GetAllQuotesAsync()).ReturnsAsync(quotes);

            //Act
            var result = await _quotesService.GetAllQuotesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 3);
        }

        [Fact]
        public async Task CreateQuotesAsync_ReturnNewId()
        {
            //Arrange
            var quotesDto = new QuotesDto
            {
                Id = 0,
                Text = "Piensa 2 veces programa 1.",
                AuthorId = 1,
                TypeId = 1,
                CreationDate = DateTime.Now
            };

            _mockQuotesRepository.Setup(repo => repo.CreateQuotesAsync(It.IsAny<Quotes>())).ReturnsAsync(1);

            //Act
            var result = await _quotesService.CreateQuotesAsync(quotesDto);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateQuotesAsync_ReturnBool()
        {
            //Arrange
            var quotesId = 1;
            var quotes = new Quotes
            {
                Id = 1,
                Text = "Piensa 2 veces programa 1.",
                AuthorId = 1,
                TypeId = 1,
                CreationDate = DateTime.Now
            };

            var quotesDto = new QuotesDto
            {
                Id = 1,
                Text = "KISS",
                AuthorId = 1,
                TypeId = 1,
                CreationDate = DateTime.Now
            };

            _mockQuotesRepository.Setup(repo => repo.GetQuotesByIdAsync(quotesId)).ReturnsAsync(quotes);

            _mockQuotesRepository.Setup(repo => repo.UpdateQuotesAsync(It.IsAny<Quotes>())).ReturnsAsync(true);

            //Act
            var result = await _quotesService.UpdateQuotesAsync(quotesId, quotesDto);

            //Asset
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteQuotesAsync_ReturnBool()
        {
            //Arrange
            var quotesId = 1;

            _mockQuotesRepository.Setup(repo => repo.DeleteQuotesAsync(quotesId)).ReturnsAsync(true);

            //Act
            var result = await _quotesService.DeleteQuotesAsync(quotesId);

            //Assert
            Assert.True(result);
        }
    }
}
