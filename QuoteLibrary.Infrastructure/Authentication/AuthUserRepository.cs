using QuoteLibrary.Domain.Interfaces;
using Dapper;
using QuoteLibrary.Domain.Entities;
using System.Data;

namespace QuoteLibrary.Infrastructure.Authentication
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AuthUserRepository(IDbConnectionFactory connectionFactory) 
        { 
            _connectionFactory = connectionFactory;       
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
