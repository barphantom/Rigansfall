namespace Rigansfall.Server.Models.Entities
{
    public class Tile
    {
        public int Id { get; set; }
        public int mapId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int type { get; set; }
        public bool isWalkable { get; set; }
    }

}
