using FCG.API.Infra.Query;
using FCG.API.Models.Entities;
using Dapper;

namespace FCG.API.Infra.Repository
{
    public class GamesRepository
    {
        private readonly DbConnectionProvider _dbProvider;
        public GamesRepository(DbConnectionProvider dbProvider) =>
            _dbProvider = dbProvider;


        public async Task<IEnumerable<Games>> GetAllGames()
        {
            using var connection = _dbProvider.GetConnection();
            var query = GamesQuery.ListAll;
            var result = await connection.QueryAsync<Games>(query);
            return result;
        }
    }
}
