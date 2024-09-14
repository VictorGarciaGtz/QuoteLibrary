using Dapper;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Infrastructure.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AuthorsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateAuthorsAsync(Authors author)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    INSERT INTO Authors (
                        Name,           BirthDate,       IdNationality,       PhotoUrl,
                        CreationDate,   ModificationDate
                    )
                    VALUES (
                        @pnName,        @pdBirthDate,    @pnIdNationality,  @psPhotoUrl,
                        @pdCreationDate,NULL
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.QuerySingleAsync<int>(sql, new
                {
                    @pnName = author.Name,
                    @pdBirthDate = author.BirthDate,
                    @pnIdNationality = author.IdNationality,
                    @psPhotoUrl = author.PhotoUrl,
                    @pdCreationDate = DateTime.Now
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return id;
            }
        }

        public async Task<bool> DeleteAuthorsAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = "DELETE FROM Authors WHERE Id = @pnId;";
                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Authors>> GetAllAuthorsAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT 
                                Id,             Name,           CreationDate, 
                                BirthDate,      IdNationality,  PhotoUrl, 
                                CreationDate,   ModificationDate 
                            FROM Authors";
                return await connection.QueryAsync<Authors>(sql, commandType: System.Data.CommandType.Text, commandTimeout: 0);
            }
        }

        public async Task<Authors?> GetAuthorsByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"SELECT 
                                Id,             Name,           CreationDate, 
                                BirthDate,      IdNationality,  PhotoUrl, 
                                CreationDate,   ModificationDate 
                            FROM Authors
                            WHERE Id = @pnId";
                return await connection.QuerySingleOrDefaultAsync<Authors>(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
            }
        }

        public async Task<bool> UpdateAuthorsAsync(Authors author)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    UPDATE Authors
                    SET Name = @pnName,
                        BirthDate = @pdBirthDate,
                        IdNationality = @pnIdNationality,
                        PhotoUrl = @psPhotoUrl,                       
                        ModificationDate = GETDATE()
                    WHERE Id = @pnId;";

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnName = author.Name,
                    @pdBirthDate = author.BirthDate,
                    @pnIdNationality = author.IdNationality,
                    @psPhotoUrl = author.PhotoUrl,
                    @pnId = author.Id,
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return rowsAffected > 0;
            }
        }
    }
}
