using Dapper;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuoteLibrary.Infrastructure.Repositories
{
    public class TypesQuotesRepository : ITypesQuotesRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TypesQuotesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateTypesQuotesAsync(TypesQuotes type)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    INSERT INTO TypesQuotes (
                        Name,    CreationDate, UserId
                    )
                    VALUES (
                        @pnName,   GETDATE(), @pnUserId
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.QuerySingleAsync<int>(sql, new
                {
                    @pnName = type.Name,
                    @pnUserId = type.UserId
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return id;
            }
        }

        public async Task<bool> DeleteTypesQuotesAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = "DELETE FROM TypesQuotes WHERE Id = @pnId;";
                var rowsAffected = await connection.ExecuteAsync(sql, new {
                    @pnId = id 
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<TypesQuotes>> GetAllTypesQuotesAsync()
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                string sql = "SELECT Id, Name, CreationDate, ModificationDate FROM TypesQuotes";
                return await connection.QueryAsync<QuoteLibrary.Domain.Entities.TypesQuotes>(sql);
            }
        }

        public async Task<TypesQuotes?> GetTypesQuotesByIdAsync(int id)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                string sql = "SELECT Id, Name, CreationDate, ModificationDate FROM TypesQuotes WHERE Id = @pnId";
                return await connection.QuerySingleOrDefaultAsync<QuoteLibrary.Domain.Entities.TypesQuotes>(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
            }
        }

        public async Task<bool> UpdateTypesQuotesAsync(TypesQuotes type)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    UPDATE TypesQuotes
                    SET Name = @pnName,
                        ModificationDate = GETDATE()
                    WHERE Id = @Id;";

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnName = type.Name,
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return rowsAffected > 0;
            }
        }
    }
}
