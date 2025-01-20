using System.Text.Json;
using Rigansfall.Server.Models.DTOs;
using Rigansfall.Server.Models.Entities;

namespace Rigansfall.Server.Models.Services
{
    public class GameLogicService
    {
        private Player _player;
        private List<Enemy> _enemies;
        private Tile[,] _mapTiles;

        public GameLogicService(string mapFilePath)
        {
            InitializeGame(mapFilePath);
        }

        public Player GetPlayer() => _player;
        public List<Enemy> GetEnemies() => _enemies;

        private void InitializeGame(string mapFilePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(mapFilePath);
                var mapData = JsonSerializer.Deserialize<Map>(jsonContent);

                if (mapData == null || mapData.tiles == null)
                    throw new Exception("Nie udało się odczytać danych mapy.");

                _mapTiles = new Tile[10, 10];
                foreach (var tile in mapData.tiles)
                {
                    _mapTiles[tile.x, tile.y] = tile;
                }

                var playerTile = mapData.tiles.FirstOrDefault(t => t.type == 1);
                if (playerTile == null)
                    throw new Exception("Nie znaleziono pozycji startowej gracza na mapie.");

                _player = new Player
                {
                    X = playerTile.x,
                    Y = playerTile.y,
                    HP = 100,
                    AttackDamage = 10,
                };

                _enemies = mapData.tiles
                    .Where(t => (t.type == 2 || t.type == 3))
                    .Select(t => new Enemy
                    {
                        X = t.x,
                        Y = t.y,
                        HP = Random.Shared.Next(15, 30),
                        AttackDamage = Random.Shared.Next(1, 5),
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd inicjalizacji gry: {ex.Message}");
            }
        }

        public bool MovePlayer(int newX, int newY)
        {
            if (!IsOneTileApart(newX, newY)) return false;
            if (!IsEnemyClicked(newX, newY)) return false;

            _player.MovePlayer(newX, newY);
            return true;

        }

        private bool IsOneTileApart(int newX, int newY)
        {
            if (Math.Abs(_player.X - newX) <= 1 && Math.Abs(_player.Y - newY) <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsEnemyClicked(int newX, int newY)
        {
            return _enemies.FirstOrDefault(enemy => enemy.X == newX && enemy.Y == newY) != null;
        }

        private Enemy GetEnemyClicked(int newX, int newY)
        {
            return _enemies.FirstOrDefault(enemy => enemy.X == newX && enemy.Y == newY);
        }

    }
}
