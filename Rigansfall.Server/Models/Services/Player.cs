namespace Rigansfall.Server.Models.Services
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int HP { get; set; }
        public int AttackDamage { get; set; }
        public void MovePlayer(int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
        }
    }
}
