using ApiCaatalogoJogos.Entities;
using ApiCaatalogoJogos.InputModel;
using ApiCaatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        public Task<List<Game>> GetGames(int page, int quantity);
        public Task<Game> GetGameById(Guid gameId);
        public Task<List<GameViewModel>> GetGames(string name, string producer);
        public Task CreateGame(Game game);
        public Task UpdateGame(Game game);
        public Task DeleteGame(Guid gameId);
    }
}
