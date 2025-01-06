using Moq;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace QuoteLibrary.Application.Tests.Services
{
    public class AuthUserServiceTests
    {
        private readonly Mock<IAuthUserRepository> _authUserRepositoryMock;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly AuthUserService _authUserService;

        public AuthUserServiceTests()
        {
            _authUserRepositoryMock = new Mock<IAuthUserRepository>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _authUserService = new AuthUserService(_authUserRepositoryMock.Object, _jwtTokenServiceMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnJwtToken_WhenUserIsValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var user = new Users { Id = 1, Username = username, PasswordHash = "hashedpassword", RoleName = "User", Email = "test@example.com" };
            var token = "generated-jwt-token";

            _authUserRepositoryMock.Setup(repo => repo.ValidateUserAsync(username, password)).ReturnsAsync(user);
            _jwtTokenServiceMock.Setup(service => service.GenerateJwtToken(username, user.RoleName, user.Id)).Returns(token);

            // Act
            var result = await _authUserService.AuthenticateAsync(username, password);

            // Assert
            Assert.Equal(token, result);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenUserIsInvalid()
        {
            // Arrange
            var username = "testuser";
            var password = "password";

            _authUserRepositoryMock.Setup(repo => repo.ValidateUserAsync(username, password)).ReturnsAsync((Users)null);

            // Act
            var result = await _authUserService.AuthenticateAsync(username, password);

            // Assert
            Assert.Null(result);
        }
    }
}