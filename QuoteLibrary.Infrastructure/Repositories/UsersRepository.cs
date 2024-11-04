using Dapper;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UsersRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateUsersAsync(Users user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"INSERT INTO Users (
                    Username, PasswordHash, RoleName, CreationDate, Email
                ) VALUES (
                    @psUsername, @psPasswordHash, @psRoleName, @pdCreationDate, @psEmail
                ) 
                SELECT CAST(SCOPE_IDENTITY() as int);";

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                var result = await connection.QuerySingleAsync<int>(sql,
                new
                {
                    @psUsername = user.Username,
                    @psPasswordHash = passwordHash,
                    @psRoleName = "User",
                    @pdCreationDate = DateTime.Now,
                    @psEmail = user.Email
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return result;
            }
        }

        public Task<bool> DeleteUsersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistUserWithUsernameOrEmail(string username, string email)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT Id, Username, PasswordHash, RoleName, CreationDate, ModificationDate 
                            FROM Users 
                            WHERE Username = @psUsername
                            OR Email = @psEmail";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(sql, new
                {
                    @psUsername = username,
                    @psEmail = email
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
             
                return user != null;
            }
        }

        public Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Authors?> GetUsersByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUsersAsync(Authors author)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUsersAsync(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
