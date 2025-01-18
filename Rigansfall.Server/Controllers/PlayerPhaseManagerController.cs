using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models;
//using Rigansfall.Server.DTOs;
using Rigansfall.Server.Models.DTOs;
using Rigansfall.Server.Models.Entities;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerPhaseManagerController : ControllerBase
    {
        private static int maxStamina = 10; // Maksymalna stamina (można rozszerzyć, aby była odczytywana z bazy danych)
        private static int currentStamina = maxStamina; // Obecna stamina
        private static (int X, int Y) playerPosition = (0, 0); // Pozycja startowa gracza na mapie
        private readonly Map currentMap; // Zakładamy, że masz klasę Map

        public PlayerPhaseManagerController()
        {
            // Inicjalizacja mapy, tutaj zakładamy statyczną mapę, ale docelowo powinna być dynamiczna
            currentMap = new Map
            {
                Id = 1,
                Name = "Test Map",
                Tiles = Enumerable.Range(0, 10)
                    .SelectMany(x => Enumerable.Range(0, 10)
                    .Select(y => new Tile
                    {
                        MapId = 1,
                        X = x,
                        Y = y,
                        isWalkable = true // Wszystkie kafelki na początku są przechodnie
                    }))
                    .ToList()
            };
        }

        /// <summary>
        /// Rozpoczyna nową turę gracza, resetując jego staminę do maksymalnej.
        /// </summary>
        [HttpPost("start-turn")]
        public IActionResult StartTurn()
        {
            currentStamina = maxStamina;
            return Ok(new { Message = "Gracz rozpoczął nową turę.", Stamina = currentStamina });
        }

        /// <summary>
        /// Obsługuje ruch gracza na wybrane pole, jeśli ma wystarczającą ilość staminy.
        /// </summary>
        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            // Sprawdzanie staminy
            if (currentStamina <= 0)
            {
                return BadRequest(new { Message = "Nie masz wystarczającej ilości staminy, aby się poruszyć. Rozpocznij nową turę." });
            }

            // Sprawdzanie poprawności współrzędnych
            if (Math.Abs(moveRequest.currentX - moveRequest.newX) > 1 || Math.Abs(moveRequest.currentY - moveRequest.newY) > 1)
            {
                return BadRequest(new { Message = "Możesz poruszać się tylko na sąsiednie pola." });
            }

            // Sprawdzanie, czy docelowe pole jest przechodnie
            var targetTile = currentMap.Tiles.FirstOrDefault(t => t.X == moveRequest.newX && t.Y == moveRequest.newY);
            if (targetTile == null || !targetTile.isWalkable)
            {
                return BadRequest(new { Message = "Docelowe pole nie jest przechodnie." });
            }

            // Aktualizacja pozycji i staminy
            playerPosition = (moveRequest.newX, moveRequest.newY);
            currentStamina--;

            return Ok(new
            {
                Message = "Gracz przesunął się na nowe pole.",
                Position = playerPosition,
                Stamina = currentStamina
            });
        }

        /// <summary>
        /// Sprawdza aktualny stan gracza, w tym pozycję i staminę.
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetPlayerStatus()
        {
            return Ok(new
            {
                Position = playerPosition,
                Stamina = currentStamina
            });
        }
    }
}
