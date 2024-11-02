using QuoteLibrary.Domain.Interfaces;
using Dapper;
using QuoteLibrary.Domain.Entities;
using System.Data;

namespace QuoteLibrary.Infrastructure.Authentication
{
    public class UserService : IUserService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserService(IDbConnectionFactory connectionFactory) 
        { 
            _connectionFactory = connectionFactory;       
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Users>("SELECT Id, Username, PasswordHash, RoleName, CreationDate, ModificationDate FROM Users WHERE Username = @psUsername", new
                {
                    @psUsername = username
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                if (user != null) { return false; }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                var result = await connection.ExecuteAsync(
                "INSERT INTO Users (Username, PasswordHash, RoleName, CreationDate) VALUES (@psUsername, @psPasswordHash, @psRoleName, @pdCreationDate)",
                new
                {
                    @psUsername = username,
                    @psPasswordHash = passwordHash,
                    @psRoleName = "User",
                    @pdCreationDate = DateTime.Now
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return result > 0;
            }
        }

        public async Task<Users?> ValidateUserAsync(string username, string password)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Users>("SELECT Id, Username, PasswordHash, RoleName, CreationDate, ModificationDate FROM Users WHERE Username = @psUsername", new
                {
                    @psUsername = username
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                if(user == null) {  return null; }

                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
            }           
        }
    }
}
