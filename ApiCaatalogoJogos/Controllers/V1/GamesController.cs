using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> GetObjects()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<object>>> GetGameById(Guid gameId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateGame(object game)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, object game)
        {
            return Ok();
        }

        [HttpPatch("{id}/price/{price}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, double price)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> DeleteGame(Guid gameId)
        {
            return Ok();
        }
    }
}
