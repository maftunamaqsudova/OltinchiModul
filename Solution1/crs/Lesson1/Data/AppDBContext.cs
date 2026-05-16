using Lesson1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson1.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {

        }
    }
}
