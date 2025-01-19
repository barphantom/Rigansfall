using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.DTOs;
using Rigansfall.Server.Models.Entities;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerPhaseManagerController : ControllerBase
    {
        private static MapManager CurrentMap; // Tymczasowe przechowywanie stanu mapy na serwerze

        // Inicjalizacja mapy (zależne od wcześniejszego MapsController)
        public PlayerPhaseManagerController()
        {
            if (CurrentMap == null)
            {
                CurrentMap = new MapManager
                {
                    mapId = 1,
                    mapName = "Gumisiowy Las",
                    Width = 10,
                    Height = 10,
                    Tiles = GenerateInitialTiles(10, 10),
                    graczX = 2,
                    graczY = 5,
                    graczMaxHP = 100,
                    graczCurrentHP = 100,
                    graczAtak = 10,
                    graczObrona = 5,
                    graczMaxStamina = 10,
                    graczCurrentStamina = 10
                };
            }
        }

        // Ruch gracza
        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            // Sprawdź, czy gracz ma wystarczająco staminy
            if (CurrentMap.graczCurrentStamina <= 0)
            {
                return BadRequest(new { success = false, message = "Brak staminy. Tura zakończona." });
            }

            // Sprawdź, czy ruch jest dozwolony
            if (Math.Abs(moveRequest.currentX - moveRequest.newX) > 1 || Math.Abs(moveRequest.currentY - moveRequest.newY) > 1)
            {
                return BadRequest(new { success = false, message = "Nie można się przemieścić o więcej niż jedno pole." });
            }

            // Sprawdź, czy kafelek jest przechodni
            var targetTile = CurrentMap.Tiles.FirstOrDefault(t => t.X == moveRequest.newX && t.Y == moveRequest.newY);
            if (targetTile == null || !targetTile.isWalkable)
            {
                return BadRequest(new { success = false, message = "Wybrane pole jest nieprzechodnie." });
            }

            // Aktualizuj pozycję gracza
            CurrentMap.graczX = moveRequest.newX;
            CurrentMap.graczY = moveRequest.newY;

            // Zmniejsz staminę
            CurrentMap.graczCurrentStamina--;

            // Sprawdź, czy stamina się skończyła
            if (CurrentMap.graczCurrentStamina <= 0)
            {
                EndTurn();
                return Ok(new { success = true, message = "Tura zakończona. Stamina się skończyła.", newPosition = new { x = moveRequest.newX, y = moveRequest.newY } });
            }

            return Ok(new { success = true, message = "Ruch wykonany.", newPosition = new { x = moveRequest.newX, y = moveRequest.newY }, remainingStamina = CurrentMap.graczCurrentStamina });
        }

        // Zakończenie tury gracza
        private void EndTurn()
        {
            // Odśwież staminę gracza
            CurrentMap.graczCurrentStamina = CurrentMap.graczMaxStamina;

            // Tu można dodać logikę dla tury przeciwników
        }

        // Pomocnicza metoda do generowania kafelków mapy
        private static List<Tile> GenerateInitialTiles(int width, int height)
        {
            return Enumerable.Range(0, width)
                .SelectMany(x => Enumerable.Range(0, height)
                .Select(y => new Tile
                {
                    X = x,
                    Y = y,
                    isWalkable = true,
                    Type = 0 // Domyślny typ kafelka
                }))
                .ToList();
        }
    }
}
