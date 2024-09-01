using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using QuoteLibrary.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace QuoteLibrary.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            var databaseProvider = _config.GetSection("DatabaseConfig:DatabaseProvider").Value;

            IDbConnection connection;

            if(databaseProvider == "SqlServer")
            {
                var connectionString = _config.GetConnectionString("SqlServerConnectionStringQL");
                connection = new SqlConnection(connectionString);

            } else if(databaseProvider == "MySql")
            {
                var connectionString = _config.GetConnectionString("MySqlConnectionStringQL");
                connection = new MySqlConnection(connectionString);

            } else
            {
                throw new InvalidOperationException("Unsupported database provider");
            }

            connection.Open();

            return  connection;
        }
    }
}
