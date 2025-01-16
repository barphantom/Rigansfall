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
                mapId = mapID,
                mapName = "Gumisiowy las",
                Width = 50,
                Height = 50,
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
        //public ActionResult<Maps> GetMaps()
        //{
        //    var newMap = new Maps
        //    {
        //        mapId = 5,
        //        mapName = "Mistyczny las"
        //    };
        //    return Ok(newMap);
        //}

        private int getMapId()
        {
            return 1; // Na razie zawsze zwraca 1
        }

    }
}
