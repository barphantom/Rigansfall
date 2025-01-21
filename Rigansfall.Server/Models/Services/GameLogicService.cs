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
        private int moves = 0;

        public GameLogicService()
        {
            string mapJSONPath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", "map3.json");
            InitializeGame(mapJSONPath);
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

        public void MovePlayer(int newX, int newY)
        {
            _player.MovePlayer(newX, newY);
            moves++;
        }

        public bool CanMovePlayer(int currentX, int currentY, int newX, int newY)
        {
            if (!IsOneTileApart(currentX, currentY, newX, newY))
            {
                return false;
            }
            else if (IsEnemyClicked(newX, newY))
            {
                return false;
            }

            Console.WriteLine($"Player pos X: {_player.X} and pos Y: {_player.Y}");
            return true;

        }

        private bool IsOneTileApart(int currentX, int currentY, int newX, int newY)
        {
            if (Math.Abs(currentX - newX) <= 1 && Math.Abs(currentY - newY) <= 1)
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
            var enemy = _enemies.FirstOrDefault(enemy => enemy.X == newX && enemy.Y == newY);
            if (enemy != null)
            {
                Console.WriteLine($"Enemy: {enemy.X} i {enemy.Y}");

            }

            return enemy != null;
        }

        private Enemy GetEnemyClicked(int newX, int newY)
        {
            return _enemies.FirstOrDefault(enemy => enemy.X == newX && enemy.Y == newY);
        }

    }
}
