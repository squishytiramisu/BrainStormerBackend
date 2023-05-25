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
    }
}
