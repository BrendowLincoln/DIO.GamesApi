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
        [Route("GetGames")]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var result = await _gameService.GetGames(page, quantity);

            if(result.Count() == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route(("GetGamesById"))]
        public async Task<ActionResult<GameViewModel>> GetGameById(Guid gameId)
        {
            var result = await _gameService.GetGameById(gameId);
            return Ok(result);
        }

        [HttpPost]
        [Route(("CreateGame"))]
        public async Task<ActionResult<GameViewModel>> CreateGame(GameInputModel game)
        {

            try
            {
                var result = await _gameService.CreateGame(game);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }

        }

        [HttpPut]
        [Route("UpdateGame/{id}")]
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

        [HttpPatch]
        [Route("UpdateGame/{id}/price/{price}")]
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

        [HttpPost]
        [Route(("Delete"))]
        public async Task<ActionResult> DeleteGame(Guid gameId)
        {

            await _gameService.DeleteGame(gameId);
            return Ok();

            //    try
            //    {

            //    }
            //    catch (Exception ex)
            //    {
            //        return NotFound("Não existe este jogo");
            //    }
            }
        }
}
