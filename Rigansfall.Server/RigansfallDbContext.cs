using Rigansfall.Server.Models.Entities;
using System.Data.Entity;

namespace Rigansfall.Server.Models
{
    internal class RigansfallDbContext : DbContext
    {
        public DbSet<Moves> Moves { get; set; }
    }
}