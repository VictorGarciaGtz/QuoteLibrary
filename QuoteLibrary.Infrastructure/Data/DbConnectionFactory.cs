using QuoteLibrary.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseConfig _config;

        public DbConnectionFactory(DatabaseConfig config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_config.ConnectionStringQL);

            connection.Open();

            return  connection;
        }
    }
}
