using ApiCaatalogoJogos.InputModel;
using ApiCaatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetGames(int page, int quantity);
        Task<GameViewModel> GetGameById(Guid gameId);
        Task<GameViewModel> CreateGame(GameInputModel game);
        Task UpdateGame(Guid gameId, GameInputModel game);
        Task UpdateGame(Guid gameId, double price);
        Task DeleteGame(Guid gameId);

    }
}
