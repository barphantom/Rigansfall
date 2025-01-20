using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.Entities;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        //[HttpGet("new-map")]
        //public ActionResult<Map> GetMap()
        //{
        //    int mapID = getMapId();
        //    // Przykładowa mapa 10x10
        //    var map = new Map
        //    {
        //        mapId = mapID,
        //        mapName = "Gumisiowy las",
        //        Width = 50,
        //        Height = 50,
        //        Tiles = Enumerable.Range(0, 10)
        //            .SelectMany(x => Enumerable.Range(0, 10)
        //            .Select(y => new Tile
        //            {
        //                MapId = mapID,
        //                X = x,
        //                Y = y,
        //                isWalkable = true // Wszystkie kafelki na początku są "przechodnie"
        //            }))
        //            .ToList()
        //    };

        //    return Ok(map);

        //}

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

        private int getMapId()
        {
            return 1;
        }
    }
}
