using Lesson2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <Student> Students { get; set; }
        public DbSet <Teacher> Teachers { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
