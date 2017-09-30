using Supero.Tasklist.Models;
using System.Data.Entity;

namespace Supero.Tasklist.Contexts
{
    public class SuperoTasklistWebAPIContext : DbContext
    {
        public SuperoTasklistWebAPIContext()
            : base("name=SuperoAzureDb")
        { }

        public virtual DbSet<Task> Task { get; set; }
    }
}