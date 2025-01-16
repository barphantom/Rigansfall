using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        [HttpGet("scenariusz{id:int}")]
        public ActionResult<Map> GetMap()
        {
            int mapID = getMapId();

            var map = GenerateMap(mapID);

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

        private Map GenerateMap(int id, string name, int width, int height)
        {
            var tiles = Enumerable.Range(0, width)
                .SelectMany(x => Enumerable.Range(0, height)
                .Select(y => new Tile
                {
                    MapId = id,
                    X = x,
                    Y = y,
                    isWalkable = DetermineWalkability(x, y) // Wywołanie logiki do określenia przechodniości
                }))
                .ToList();

            return new Map
            {
                mapId = id,
                mapName = name,
                Tiles = tiles
            };
        }
        private int getMapId()
        {
            return 1; // Na razie zawsze zwraca 1
        }

    }
}
