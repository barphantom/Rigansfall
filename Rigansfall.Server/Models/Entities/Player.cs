namespace Rigansfall.Server.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MaxHealth { get; set; }
        public int MaxSpeed { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int XP { get; set; }
    }
}
