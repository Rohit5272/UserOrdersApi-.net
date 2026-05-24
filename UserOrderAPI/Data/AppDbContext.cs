using Microsoft.EntityFrameworkCore;
using UserOrderAPI.Models;

namespace UserOrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<FileDocument> FileDocuments { get; set; }
    }
}
