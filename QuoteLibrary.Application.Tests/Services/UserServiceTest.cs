using Moq;
using QuoteLibrary.Application.DTOs.User;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.Services;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Tests.Services
{
    public class UserServiceTest
    {
        private readonly UsersService _usersService;

        private readonly Mock<IUsersRepository> _mockUsersRepository;
        private readonly Mock<ITokenService> _mockTokenService;

        public UserServiceTest()
        {
            _mockUsersRepository = new Mock<IUsersRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _usersService = new UsersService(_mockUsersRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsZero_WhenUserExists()
        {
            //Arrange
            var userDto = new CreateUserDto
            {
                Username = "user1",
                Email = "usuer1@gmail.com",
                PasswordHash = "user1"
            };

            var user = new Users
            {
                Id = 0,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                RoleName = "User",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now
            };

            _mockUsersRepository.Setup(repo => repo.ExistUserWithUsernameOrEmail(userDto.Username, userDto.Email)).ReturnsAsync(true);

            _mockUsersRepository.Setup(repo => repo.CreateUsersAsync(user)).ReturnsAsync(0);

            //Act
            var result = await _usersService.CreateUserAsync(userDto);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsUserId_WhenUserNotExists()
        {
            //Arrange
            var userDto = new CreateUserDto
            {
                Username = "user1",
                Email = "user1@gmail.com",
                PasswordHash = "user1"
            };


            _mockUsersRepository.Setup(repo => repo.ExistUserWithUsernameOrEmail(userDto.Username, userDto.Email)).ReturnsAsync(false);

            _mockUsersRepository.Setup(repo => repo.CreateUsersAsync(It.IsAny<Users>())).ReturnsAsync(5);

            //Act
            var result = await _usersService.CreateUserAsync(userDto);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnFalse_WhenUserIsNotDeleted_AndRoleIsAdmin()
        {
            //Arrange
            var userIdAdmin = 1;
            var userId = 10;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userIdAdmin.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("Admin");

            _mockUsersRepository.Setup(repo => repo.DeleteUsersAsync(userId)).ReturnsAsync(false);

            //Act
            var result = await _usersService.DeleteUsersAsync(userId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnFalse_WhenUserIsNotDeleted_AndRoleIsUser()
        {
            //Arrange
            var userIdRoleUser = 2;
            var userId = 2;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userIdRoleUser.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            _mockUsersRepository.Setup(repo => repo.DeleteUsersAsync(userId)).ReturnsAsync(false);

            //Act
            var result = await _usersService.DeleteUsersAsync(userId);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnTrue_WhenUserIsDeleted_AndRoleIsAdmin()
        {
            //Arrange
            var userIdAdmin = 1;
            var userId = 10;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userIdAdmin.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("Admin");

            _mockUsersRepository.Setup(repo => repo.DeleteUsersAsync(userId)).ReturnsAsync(true);

            //Act
            var result = await _usersService.DeleteUsersAsync(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnTrue_WhenUserIsDeleted_AndRoleIsUser()
        {
            //Arrange
            var userIdRoleUser = 2;
            var userId = 2;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userIdRoleUser.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            _mockUsersRepository.Setup(repo => repo.DeleteUsersAsync(userId)).ReturnsAsync(true);

            //Act
            var result = await _usersService.DeleteUsersAsync(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnException_WhenUserIsNotAuthorized()
        {
            //Arrange
            var userId = 10;
            var message = string.Format("This user is not authorized to perform this action to the element with Id {0}.", userId);

            _mockTokenService.Setup(service => service.GetUserId()).Returns(0.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("");

            _mockUsersRepository.Setup(repo => repo.DeleteUsersAsync(userId)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _usersService.DeleteUsersAsync(userId);
            });

            //Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnTrue_WhenUserIsAuthorized_AndEmailIsDifferent()
        {
            //Arrange
            var userId = 10;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userId.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            var userUpdate = new UpdateUserDto()
            {
                Username = "Test",
                Email = "Test@gmail.com",
                PasswordHash = Guid.NewGuid().ToString()
            };

            var user = new Users()
            {
                Id = userId,
                Username = "Test",
                Email = "user1@gmail.com",
                PasswordHash = "user1",
                RoleName = "User",
                ModificationDate = DateTime.Now,
                CreationDate = DateTime.Now,
            };

            _mockUsersRepository.Setup(service => service.GetUsersByIdAsync(userId)).ReturnsAsync(user);

            if(!(user.Email == userUpdate.Email && user.Username == userUpdate.Username))
            {
                if(user.Username == userUpdate.Username)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameEmail(userId, userUpdate.Email)).ReturnsAsync(false);
                }
                else if (user.Email == userUpdate.Email)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameUsername(userId, userUpdate.Username)).ReturnsAsync(false);
                }
                else
                {
                    _mockUsersRepository.Setup(service => service.ExistUserWithUsernameOrEmail(userUpdate.Username, userUpdate.Email)).ReturnsAsync(true);
                }
            }

            _mockUsersRepository.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>())).ReturnsAsync(true);

            //Act
            var result = await _usersService.UpdateUsersAsync(userId, userUpdate);

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnTrue_WhenUserIsAuthorized_AndUsernameIsDifferent()
        {
            //Arrange
            var userId = 10;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userId.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            var userUpdate = new UpdateUserDto()
            {
                Username = "Test",
                Email = "Test@gmail.com",
                PasswordHash = Guid.NewGuid().ToString()
            };

            var user = new Users()
            {
                Id = userId,
                Username = "User1",
                Email = "Test@gmail.com",
                PasswordHash = "user1",
                RoleName = "User",
                ModificationDate = DateTime.Now,
                CreationDate = DateTime.Now,
            };

            _mockUsersRepository.Setup(service => service.GetUsersByIdAsync(userId)).ReturnsAsync(user);

            if (!(user.Email == userUpdate.Email && user.Username == userUpdate.Username))
            {
                if (user.Username == userUpdate.Username)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameEmail(userId, userUpdate.Email)).ReturnsAsync(false);
                }
                else if (user.Email == userUpdate.Email)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameUsername(userId, userUpdate.Username)).ReturnsAsync(false);
                }
                else
                {
                    _mockUsersRepository.Setup(service => service.ExistUserWithUsernameOrEmail(userUpdate.Username, userUpdate.Email)).ReturnsAsync(true);
                }
            }

            _mockUsersRepository.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>())).ReturnsAsync(true);

            //Act
            var result = await _usersService.UpdateUsersAsync(userId, userUpdate);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnFalse_WhenUserIsAuthorized_AndUsernameExistsInOtherUser()
        {
            //Arrange
            var userId = 10;

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userId.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            var userUpdate = new UpdateUserDto()
            {
                Username = "Test",
                Email = "Test@gmail.com",
                PasswordHash = Guid.NewGuid().ToString()
            };

            var user = new Users()
            {
                Id = userId,
                Username = "User1",
                Email = "Test@gmail.com",
                PasswordHash = "user1",
                RoleName = "User",
                ModificationDate = DateTime.Now,
                CreationDate = DateTime.Now,
            };

            _mockUsersRepository.Setup(service => service.GetUsersByIdAsync(userId)).ReturnsAsync(user);

            if (!(user.Email == userUpdate.Email && user.Username == userUpdate.Username))
            {
                if (user.Username == userUpdate.Username)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameEmail(userId, userUpdate.Email)).ReturnsAsync(false);
                }
                else if (user.Email == userUpdate.Email)
                {
                    _mockUsersRepository.Setup(service => service.ExistsOtherUserWithSameUsername(userId, userUpdate.Username)).ReturnsAsync(true);
                } else
                {
                    _mockUsersRepository.Setup(service => service.ExistUserWithUsernameOrEmail(userUpdate.Username, userUpdate.Email)).ReturnsAsync(true);
                }
            }

            _mockUsersRepository.Setup(service => service.UpdateUsersAsync(It.IsAny<Users>())).ReturnsAsync(true);

            //Act
            var result = await _usersService.UpdateUsersAsync(userId, userUpdate);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserIsAthorized()
        {
            //Arrange
            var userId = 10;
            _mockTokenService.Setup(service => service.GetUserId()).Returns(userId.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");

            var user = new Users()
            {
                Id = userId,
                Username = "User1",
                Email = "User1@gmail.com",
                PasswordHash = "user1",
                RoleName = "User",
                ModificationDate = DateTime.Now,
                CreationDate = DateTime.Now,

            };

            _mockUsersRepository.Setup(service => service.GetUsersByIdAsync(userId)).ReturnsAsync(user);

            //Act
            var result = await _usersService.GetUsersByIdAsync(userId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("User1", result.Username);
            Assert.Equal("User1@gmail.com", result.Email);
        }

        [Fact]
        public async Task GetAllUsersAsync_WhenUserIsAuthorized()
        {
            //Arrange
            var userId = 10;

            var users = new List<Users>
            {
                new Users
                {
                    Id = 1,
                    Username = "User1",
                    Email = "User1@gmail.com",
                    PasswordHash = "user1",
                    ModificationDate = DateTime.Now,
                    RoleName = "User",
                    CreationDate = DateTime.Now
                },
                new Users
                {
                    Id = 2,
                    Username = "User2",
                    Email = "User2@gmail.com",
                    PasswordHash = "user2",
                    ModificationDate = DateTime.Now,
                    RoleName = "User",
                    CreationDate = DateTime.Now
                },
                new Users
                {
                    Id = 3,
                    Username = "User3",
                    Email = "User3@gmail.com",
                    PasswordHash = "user3",
                    ModificationDate = DateTime.Now,
                    RoleName = "User",
                    CreationDate = DateTime.Now
                }
            };

            _mockTokenService.Setup(service => service.GetUserId()).Returns(userId.ToString());
            _mockTokenService.Setup(service => service.GetUserRole()).Returns("User");
            _mockUsersRepository.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            //Act
            var result = await _usersService.GetAllUsersAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 3);
        }
    }
}
