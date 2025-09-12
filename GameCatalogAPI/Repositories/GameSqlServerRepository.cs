using GameCatalogAPI.Entities;
using System.Data.SqlClient;

namespace GameCatalogAPI.Repositories
{
    public class GameSqlServerRepository
    {
        private readonly string sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = configuration.GetConnectionString("Default");
        }

        public async Task<List<Game>> Get(int page, int qtn)
        {
            var games = new List<Game>();

            var commandText = @"
                SELECT * 
                FROM Games 
                ORDER BY Id 
                OFFSET @Offset ROWS 
                FETCH NEXT @Qtn ROWS ONLY";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Offset", (page - 1) * qtn);
            sqlCommand.Parameters.AddWithValue("@Qtn", qtn);

            using var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (await sqlDataReader.ReadAsync())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = Convert.ToDouble(sqlDataReader["Price"])
                });
            }

            return games;
        }

        public async Task<List<Game>> Get(string name, string producer)
        {
            var games = new List<Game>();
            var commandText = "SELECT * FROM Games WHERE Name = @Name AND Producer = @Producer";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Producer", producer);

            using var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (await sqlDataReader.ReadAsync())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = Convert.ToDouble(sqlDataReader["Price"])
                });
            }

            return games;
        }

        public async Task<Game?> Get(Guid id)
        {
            Game? game = null;
            var commandText = "SELECT * FROM Games WHERE Id = @Id";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            using var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            if (await sqlDataReader.ReadAsync())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = Convert.ToDouble(sqlDataReader["Price"])
                };
            }

            return game;
        }

        public async Task Insert(Game game)
        {
            var commandText = "INSERT INTO Games (Id, Name, Producer, Price) VALUES (@Id, @Name, @Producer, @Price)";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", game.Id);
            sqlCommand.Parameters.AddWithValue("@Name", game.Name);
            sqlCommand.Parameters.AddWithValue("@Producer", game.Producer);
            sqlCommand.Parameters.AddWithValue("@Price", game.Price);

            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async Task Update(Game game)
        {
            var commandText = "UPDATE Games SET Name = @Name, Producer = @Producer, Price = @Price WHERE Id = @Id";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", game.Id);
            sqlCommand.Parameters.AddWithValue("@Name", game.Name);
            sqlCommand.Parameters.AddWithValue("@Producer", game.Producer);
            sqlCommand.Parameters.AddWithValue("@Price", game.Price);

            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async Task Delete(Guid id)
        {
            var commandText = "DELETE FROM Games WHERE Id = @Id";

            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            await sqlCommand.ExecuteNonQueryAsync();
        }
        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection ?.Dispose();
        }
    }
}
