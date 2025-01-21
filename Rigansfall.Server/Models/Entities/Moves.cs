using System.ComponentModel.DataAnnotations;


namespace Rigansfall.Server.Models.Entities
{
    public class Moves
    {
        [Key] int playerID { get; set; }
        int movesNumber { get; set; }
    }
}
