namespace Rigansfall.Server.Models
{
    public class Enemy
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public string? Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Resilience { get; set; }
    }
}
