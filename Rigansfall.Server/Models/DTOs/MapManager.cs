using Rigansfall.Server.Models.Entities;

namespace Rigansfall.Server.Models.DTOs
{
    public class MapManager
    {
        public int mapId { get; set; }
        public string? mapName { get; set; }
        public string? mapDescription { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> Tiles { get; set; }
        // dane o graczu
        public int graczX { get; set; }
        public int graczY { get; set; }
        public int graczMaxHP { get; set; }
        public int graczAtak { get; set; }
        public int graczObrona { get; set; }
        public int graczMaxStamina { get; set; }
    }
}
