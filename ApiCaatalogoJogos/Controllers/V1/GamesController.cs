using ApiCaatalogoJogos.InputModel;
using ApiCaatalogoJogos.Services;
using ApiCaatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {

        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var result = await _gameService.GetGames();

            if(result.Count() == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameViewModel>> GetGameById([FromRoute] Guid gameId)
        {
            var result = await _gameService.GetGameById(gameId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> CreateGame([FromBody] GameInputModel game)
        {
            try
            {
                var result = await _gameService.CreateGame(game);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGame(gameId, game);
                return Ok();
            } 
            catch(Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{id}/price/{price}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGame(gameId, price);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.DeleteGame(gameId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
