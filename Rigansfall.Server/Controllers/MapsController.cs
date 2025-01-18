using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.Entities;
using Rigansfall.Server.Models.DTOs;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        [HttpGet("new-map")]
        public ActionResult<MapManager> GetMap()
        {
            int mapID = getMapId();
            // Przykładowa mapa 10x10
            var map = new MapManager
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
                    .ToList(),

                //poczatkowy stan gracza
                graczX = 2,
                graczY = 5,
                graczMaxHP = 100,
                graczAtak = 10,
                graczObrona = 5,
                graczMaxStamina = 10

            };

            return Ok(map);

        }

        private int getMapId()
        {
            return 1;
        }


        //[HttpGet("scenariusz{id:int}")]
        //public ActionResult<Map> GetMap()
        //{


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
}
