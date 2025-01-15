using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        [HttpGet("new-map")]
        public ActionResult<Map> GetMap()
        {
            int mapID = getMapId();
            // Przykładowa mapa 10x10
            var map = new Map
            {
                Id = mapID,
                Name = "Test Map",
                Tiles = Enumerable.Range(0, 10)
                    .SelectMany(x => Enumerable.Range(0, 10)
                    .Select(y => new Tile
                    {
                        MapId = mapID,
                        X = x,
                        Y = y,
                        isWalkable = true // Wszystkie kafelki na początku są "przechodnie"
                    }))
                    .ToList()
            };

            return Ok(map);
            
        }

        private int getMapId()
        {
            return 1; // Na razie zawsze zwraca 1
        }

    }
}
