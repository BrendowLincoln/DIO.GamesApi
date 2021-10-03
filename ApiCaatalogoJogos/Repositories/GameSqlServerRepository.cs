using ApiCaatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {

        public readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        
        public async Task CreateGame(Game game)
        {
            var command = $"INSERT INTO Game (Id, Name, Producer, Price) VALUES ('{game.Id}', '{game.Name}', '{game.Producer}', {Double.Parse(game.Price.ToString().Replace(',', '.'))})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task DeleteGame(Guid gameId)
        {
            var command = $"DELETE FROM Game WHERE Id = '{gameId}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {

            sqlConnection?.Close();
            sqlConnection?.Dispose();

        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            Game game = null;

            var command = $"select * from Game where Id = '{gameId}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game()
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double) sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;

        }

        public async Task<List<Game>> GetGames(int page, int quantity)
        {
            var games = new List<Game>();

            var command = $"SELECT * FROM Game ORDER BY Id OFFSET {((page - 1) * quantity)} rows fetch next {quantity} rows only";


            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();


            while(sqlDataReader.Read())
            {
                games.Add(new Game()
                {
                    Id = (Guid) sqlDataReader["Id"],
                    Name = (string) sqlDataReader["Name"],
                    Producer = (string) sqlDataReader["Producer"],
                    Price = (double) sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<List<Game>> GetGames(string name, string producer)
        {
            var games = new List<Game>();

            var command = $"select * from Game where Name = '{name}' and Producer = '{producer}' ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game()
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task UpdateGame(Game game)
        {
            var command = $"UPDATE Game SET Name = '{game.Name}', Producer = '{game.Producer}', Price = {game.Price} WHERE Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
