using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.DTOs;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoveRequestController : Controller
    {
        private readonly MapStateService _mapStateService;

        public MoveRequestController(MapStateService mapStateService)
        {
            _mapStateService = mapStateService;
        }

        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            var map = _mapStateService.GetMap();
            if (map == null)
            {
                return BadRequest(new { Message = "Mapa nie została zainicjalizowana." });
            }

            if (_mapStateService.TryMovePlayer(moveRequest.newX, moveRequest.newY))
            {
                return Ok(new
                {
                    Message = "Gracz przesunął się na nowe pole.",
                    Position = new { X = map.graczX, Y = map.graczY },
                    Stamina = map.graczMaxStamina
                });
            }

            return BadRequest(new { Message = "Nie można wykonać ruchu." });
        }

        [HttpPost("start-turn")]
        public IActionResult StartTurn()
        {
            _mapStateService.ResetStamina();
            var map = _mapStateService.GetMap();
            return Ok(new
            {
                Message = "Nowa tura rozpoczęta.",
                Stamina = map?.graczMaxStamina
            });
        }
    }
}
