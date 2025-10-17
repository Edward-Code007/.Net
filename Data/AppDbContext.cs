using Microsoft.EntityFrameworkCore;
using tests.Models;

namespace tests.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> User { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
