using Rigansfall.Server.Models.Entities;
using System;

namespace Rigansfall.Server.Models.DTOs
{
    public class MapManager
    {
        public int mapId { get; set; }
        public string? mapName { get; set; }
        public string? mapDescription { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> Tiles { get; set; } = new List<Tile>();

        // Dane o graczu
        public int graczX { get; set; }
        public int graczY { get; set; }
        public int graczMaxHP { get; set; }
        public int graczCurrentHP { get; set; }
        public int graczAtak { get; set; }
        public int graczObrona { get; set; }
        public int graczMaxStamina { get; set; }
        public int graczCurrentStamina { get; set; }

        // Stan mapy i gry
        public bool IsGameOver { get; set; }
        public string? CurrentPhase { get; set; } // "PlayerTurn", "EnemyTurn", etc.

        // Przeciwnicy
        //public List<Enemy> Enemies { get; set; } = new List<Enemy>();

        // Historia tur
        //public List<TurnLog> TurnHistory { get; set; } = new List<TurnLog>();

    }
}
