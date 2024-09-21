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
    public class QuotesRepository : IQuotesRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public QuotesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateQuotesAsync(Quotes quote)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    INSERT INTO Quotes(
	                    Text,				AuthorId,		TypeId,
	                    CreationDate
                    ) VALUES (
	                    @psText,			@pnAuthorId,	@pnTypeId,
	                    GETDATE()
                    )
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.QuerySingleAsync<int>(sql, new
                {
                    @psText = quote.Text,
                    @pnAuthorId = quote.AuthorId,
                    @pnTypeId = quote.TypeId
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return id;
            }
        }

        public async Task<bool> DeleteQuotesAsync(int id)
        {
            string sql = @"DELETE FROM Quotes WHERE Id = @pnId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Quotes>> GetAllQuotesAsync()
        {
            string sql = @"SELECT 
	                            Id,
	                            Text,
	                            AuthorId,
	                            TypeId,
	                            CreationDate,
	                            ModificationDate
                            FROM Quotes";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var quotes = await connection.QueryAsync<Quotes>(sql, new
                {
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return quotes;
            }
        }

        public async Task<Quotes?> GetQuotesByIdAsync(int id)
        {
            string sql = @"SELECT 
	                            Id,
	                            Text,
	                            AuthorId,
	                            TypeId,
	                            CreationDate,
	                            ModificationDate
                            FROM Quotes
                            WHERE Id = @pnId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var quote = await connection.QueryFirstOrDefaultAsync<Quotes>(sql, new
                {
                    @pnId = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);

                return quote;
            }
        }

        public async Task<bool> UpdateQuotesAsync(Quotes quote)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = @"
                    UPDATE Quotes
                    SET Text = @psText,
	                    AuthorId = @pnAuthorId,
	                    TypeId = @pnTypeId,
	                    ModificationDate = GETDATE()
                    WHERE Id = @pnId;";

                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    @psText = quote.Text,
                    @pnAuthorId = quote.AuthorId,
                    @pnTypeId = quote.TypeId
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
                return rowsAffected > 0;
            }
        }
    }
}

