using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.DTOs;
using Rigansfall.Server.Models.Entities;
using Rigansfall.Server.Models.Services;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameLogicService _gameLogicService;

        public GameController()
        {
            string mapJSONPath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", "map3.json");
            _gameLogicService = new GameLogicService();
        }

        [HttpGet("player")]
        public IActionResult GetPlayer()
        {
            var player = _gameLogicService.GetPlayer();
            return Ok(player);
        }

        [HttpGet("enemies")]
        public IActionResult GetEnemies()
        {
            var enemies = _gameLogicService.GetEnemies();
            return Ok(enemies);
        }

        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            if (_gameLogicService.CanMovePlayer(moveRequest.currentX, moveRequest.currentY, moveRequest.newX, moveRequest.newY))
            {
                _gameLogicService.MovePlayer(moveRequest.newX, moveRequest.newY);
                return Ok(new { canMove = true, reason = $"moved one tile X: {_gameLogicService.GetPlayer().X}, new X: {moveRequest.newX}, Y: {_gameLogicService.GetPlayer().Y}, new Y: {moveRequest.newY}" });
            } else
            {
                return Ok(new { canMove = false, reason = $"too far X: {_gameLogicService.GetPlayer().X}, new X: {moveRequest.newX}, Y: {_gameLogicService.GetPlayer().Y}, new Y: {moveRequest.newY}" });
            }
        }

        [HttpGet("load-map")]
        public ActionResult<Map> GetMapFromFile()
        {
            try
            {
                // Ścieżka do pliku
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", "map3.json");

                // Odczytaj zawartość pliku
                string jsonContent = System.IO.File.ReadAllText(filePath);

                // Deserializuj JSON do obiektu Map
                var map = JsonSerializer.Deserialize<Map>(jsonContent);
                Console.WriteLine(map);
                Console.WriteLine(map.tiles[0].type);


                if (map == null)
                {
                    return NotFound("Map data could not be loaded.");
                }

                // Zwróć mapę
                return Ok(map);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading map: {ex.Message}");
            }
        }
    }
}
