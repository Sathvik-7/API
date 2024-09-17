using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoAPIProject.DataDbContext
{
    public class WalksDbContext : DbContext
    {

        public WalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Region> Regions { get; set; }
    }
}
