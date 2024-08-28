using Dapper;
using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Infrastructure.Repositories
{
    public class TypesQuotesRepository : ITypesQuotesRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TypesQuotesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<QuoteLibrary.Domain.Entities.TypesQuotes>> GetAllTypesQuotesAsync()
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                string sql = "SELECT Id, Name FROM TypesQuotes";
                return await connection.QueryAsync<QuoteLibrary.Domain.Entities.TypesQuotes>(sql);
            }
        }

        public async Task<QuoteLibrary.Domain.Entities.TypesQuotes?> GetTypeQuotesByIdAsync(int id)
        {
            using(var connection = _connectionFactory.CreateConnection())
            {
                string sql = "SELECT Id, Name FROM TypesQuotes WHERE Id = @id";
                return await connection.QuerySingleOrDefaultAsync<QuoteLibrary.Domain.Entities.TypesQuotes>(sql, new
                {
                    @id = id
                }, commandType: System.Data.CommandType.Text, commandTimeout: 0);
            }
        }
    }
}
