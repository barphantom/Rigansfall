namespace Rigansfall.Server.Models.DTOs
{
    public class MoveRequest
    {
        public int currentX { get; set; }
        public int currentY { get; set; }
        public int newX { get; set; }
        public int newY { get; set; }
    }
}
