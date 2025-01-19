using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.Entities;
using Rigansfall.Server.Models.DTOs;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly MapStateService _mapStateService;

        public MapsController(MapStateService mapStateService)
        {
            _mapStateService = mapStateService;
        }

        [HttpGet("new-map")]
        public ActionResult<MapManager> GetMap()
        {
            int mapID = getMapId();
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
                        isWalkable = true
                    }))
                    .ToList(),
                graczX = 2,
                graczY = 5,
                graczMaxHP = 100,
                graczAtak = 10,
                graczObrona = 5,
                graczMaxStamina = 10
            };

            _mapStateService.SetMap(map);

            return Ok(map);
        }

        private int getMapId()
        {
            return 1;
        }
    }
}
