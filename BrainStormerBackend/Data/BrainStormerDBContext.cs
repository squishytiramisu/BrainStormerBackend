using BrainStormerBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Data
{
    public class BrainStormerDBContext : DbContext
    {
        public BrainStormerDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<BrainStorm> BrainStorms { get; set; }

        public DbSet<ActionStep> ActionSteps { get; set; }
    }
}
