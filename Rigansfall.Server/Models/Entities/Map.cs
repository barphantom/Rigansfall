using System.ComponentModel.DataAnnotations;

namespace Rigansfall.Server.Models.Entities
{
    public class Map
    {
        [Key] public int mapId { get; set; }
        public string? name { get; set; }
        public string? mapDescription { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> tiles { get; set; }
    }
}
