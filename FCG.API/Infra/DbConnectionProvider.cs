using FCG.API.Infra.Config;
using Microsoft.Extensions.Options;
using System.Data;
using Npgsql;

namespace FCG.API.Infra
{
    public class DbConnectionProvider(IOptions<ConnectionStrings> connections) : IDisposable
    {
        private readonly string _connectionString = connections.Value.PostgreSQL;
        private IDbConnection _connection;

        public IDbConnection GetConnection()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
            
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
