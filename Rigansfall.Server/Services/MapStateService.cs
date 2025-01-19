using Rigansfall.Server.Models.DTOs;
using Rigansfall.Server.Models.Entities;

public class MapStateService
{
    private MapManager? _currentMap;

    public MapManager? GetMap() => _currentMap;

    public void SetMap(MapManager map)
    {
        _currentMap = map;
    }

    public bool TryMovePlayer(int newX, int newY)
    {
        if (_currentMap == null) return false;

        // Sprawdź, czy gracz ma wystarczającą staminę
        if (_currentMap.graczMaxStamina <= 0)
        {
            return false;
        }

        // Sprawdź, czy ruch jest na sąsiednie pole
        if (Math.Abs(newX - _currentMap.graczX) > 1 || Math.Abs(newY - _currentMap.graczY) > 1)
        {
            return false;
        }

        // Sprawdź, czy docelowy kafelek jest przechodni
        var targetTile = _currentMap.Tiles.FirstOrDefault(t => t.X == newX && t.Y == newY);
        if (targetTile == null || !targetTile.isWalkable)
        {
            return false;
        }

        // Aktualizuj pozycję gracza i zmniejsz staminę
        _currentMap.graczX = newX;
        _currentMap.graczY = newY;
        _currentMap.graczMaxStamina -= 1;

        return true;
    }

    public void ResetStamina()
    {
        if (_currentMap != null)
        {
            _currentMap.graczMaxStamina = 10; // Lub odczytuj z bazy danych w przyszłości
        }
    }
}
