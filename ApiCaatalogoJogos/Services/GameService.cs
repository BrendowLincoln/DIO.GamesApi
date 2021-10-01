using ApiCaatalogoJogos.Entities;
using ApiCaatalogoJogos.Exceptions;
using ApiCaatalogoJogos.InputModel;
using ApiCaatalogoJogos.Repositories;
using ApiCaatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameViewModel> CreateGame(GameInputModel game)
        {
            var gameEntity = await _gameRepository.GetGames(game.Name, game.Producer);

            if (gameEntity.Count > 0)
            {
                throw new GameAlredyRegisteredException();
            }

            var gameInsert = new Game()
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.CreateGame(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public Task DeleteGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<GameViewModel> GetGameById(Guid gameId)
        {
            var game = await _gameRepository.GetGameById(gameId);

            return game == null ? game : new GameViewModel()
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<IEnumerable<GameViewModel>> GetGames(int page, int quantity)
        {
            var games = await _gameRepository.GetGames(page, quantity);

            return games.Select(game => new GameViewModel()
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price

            }).ToList(); 

        }

        public async Task UpdateGame(Guid gameId, GameInputModel game)
        {
            var gameEntity = await _gameRepository.GetGameById(gameId);

            if (gameEntity == null)
            {
                throw new GameNotRegisteredException();
            }

            gameEntity.Name = game.Name;
            gameEntity.Producer = game.Producer;
            gameEntity.Price = game.Price;

            await _gameRepository.UpdateGame(gameEntity);

        }

        public async Task UpdateGame(Guid gameId, double price)
        {
            var gameEntity = await _gameRepository.GetGameById(gameId);

            if (gameEntity == null)
            {
                throw new GameNotRegisteredException();
            }

            gameEntity.Price = price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        public void Dispose()
        {
            _gameRepository.Dispose();
        }
    }
}
