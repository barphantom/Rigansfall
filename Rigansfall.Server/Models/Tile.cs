﻿namespace Rigansfall.Server.Models
{
    public class Tile
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string? Type { get; set; }
    }

}
