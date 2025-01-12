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
                    Username, PasswordHash, RoleName, CreationDate, Email, Active
                ) VALUES (
                    @psUsername, @psPasswordHash, @psRoleName, GETDATE(), @psEmail, 1
                ) 
                SELECT CAST(SCOPE_IDENTITY() as int);";

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                var result = await connection.QuerySingleAsync<int>(sql,
                new
                {
                    @psUsername = user.Username,
                    @psPasswordHash = passwordHash,
                    @psRoleName = "User",
                    @psEmail = user.Email
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return result;
            }
        }

        public async Task<bool> DeleteUsersAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"UPDATE Users SET Active = 0, ModificationDate = GETDATE() WHERE Id = @pnId;";

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> ExistsOtherUserWithSameEmail(int id, string email)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT Id, Username, PasswordHash, RoleName, CreationDate, ModificationDate 
                            FROM Users 
                            WHERE Email = @psEmail
                            AND Id != @pnId";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(sql, new
                {
                    @psEmail = email,
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return user != null;
            }
        }

        public async Task<bool> ExistsOtherUserWithSameUsername(int id, string username)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT Id, Username, PasswordHash, RoleName, CreationDate, ModificationDate 
                            FROM Users 
                            WHERE Email = @psUsername
                            AND Id != @pnId";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(sql, new
                {
                    @psUsername = username,
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return user != null;
            }
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

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT Id, Username, RoleName, CreationDate, ModificationDate, Email 
                            FROM Users 
                            WHERE RoleName = 'User'";

                var users = await connection.QueryAsync<Users>(sql, new {
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return users;
            }
        }

        public async Task<Users?> GetUsersByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT Id, Username, RoleName, CreationDate, ModificationDate, Email 
                            FROM Users 
                            WHERE Id = @pnId";

                var users = await connection.QueryFirstOrDefaultAsync<Users>(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return users;
            }
        }

        public async Task<bool> UpdateUsersAsync(Users user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"UPDATE Users SET Username = @psUsername, Email = @psEmail, ModificationDate = GETDATE(), PasswordHash = @psPasswordHash WHERE Id = @pnId;";

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnId = user.Id,
                    @psUsername = user.Username,
                    @psEmail = user.Email,
                    @psPasswordHash = passwordHash
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return rowsAffected > 0;
            }
        }
    }
}
