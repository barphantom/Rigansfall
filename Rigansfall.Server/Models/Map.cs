﻿using System.ComponentModel.DataAnnotations;

namespace Rigansfall.Server.Models
{
    public class Map
    {
        [Key]public int mapId { get; set; }
        public string? mapName { get; set; }
        public string? mapDescription { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> Tiles { get; set; }
    }
}
