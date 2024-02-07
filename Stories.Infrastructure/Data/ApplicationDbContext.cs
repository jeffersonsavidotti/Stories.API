using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;

namespace Stories.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Story> stories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Vote> votes { get; set; }
    }
}
