﻿namespace Rigansfall.Server.Models
{
    public class MoveRequest
    {
        public int currentStamina { get; set; }
        public int currentX { get; set; }
        public int currentY { get; set; }
        public int newX { get; set; }
        public int newY { get; set; }
    }
}
