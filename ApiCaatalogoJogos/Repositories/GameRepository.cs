using ApiCaatalogoJogos.Entities;
using ApiCaatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Repositories
{
    public class GameRepository : IGameRepository
    {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("e19e2a69-f511-e799-4b74-2c6405c382a3"), new Game(){Id = Guid.Parse("e19e2a69-f511-e799-4b74-2c6405c382a3"), Name = "Fifa 21", Producer = "EA", Price = 151.21}},
            {Guid.Parse("2fa66083-b7f1-ac3b-844e-26ba51beb724"), new Game(){Id = Guid.Parse("2fa66083-b7f1-ac3b-844e-26ba51beb724"), Name = "Fifa 20", Producer = "EA", Price = 141.21}},
            {Guid.Parse("16ddcb89-1a94-f86b-83d5-066880aa1bbe"), new Game(){Id = Guid.Parse("16ddcb89-1a94-f86b-83d5-066880aa1bbe"), Name = "Fifa 19", Producer = "EA", Price = 131.21}},
            {Guid.Parse("8f022da3-c54a-937f-cd4d-d013665bfc7e"), new Game(){Id = Guid.Parse("8f022da3-c54a-937f-cd4d-d013665bfc7e"), Name = "Fifa 18", Producer = "EA", Price = 121.21}},
            {Guid.Parse("6ccd08e8-dd8d-94d7-ca19-778ddd1d6623"), new Game(){Id = Guid.Parse("6ccd08e8-dd8d-94d7-ca19-778ddd1d6623"), Name = "Street Fighter V", Producer = "EA", Price = 100.35}},
            {Guid.Parse("44b3ae6c-145d-03f8-9a5a-fcf90354ce98"), new Game(){Id = Guid.Parse("44b3ae6c-145d-03f8-9a5a-fcf90354ce98"), Name = "Grand Theft Auto V", Producer = "Rockstar", Price = 190.32}}
        };

        public Task CreateGame(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task DeleteGame(Guid gameId)
        {
            games.Remove(gameId);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //
        }

        public Task<Game> GetGameById(Guid gameId)
        {

            if(!games.ContainsKey(gameId))
            {
                return null;
            }

            return Task.FromResult(games[gameId]);
        }

        public Task<List<Game>> GetGames(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<List<Game>> GetGames(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task UpdateGame(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
    }
}
