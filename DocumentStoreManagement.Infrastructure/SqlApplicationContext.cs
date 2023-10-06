using DocumentStoreManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Infrastructure
{
    public class SqlApplicationContext : DbContext
    {
        public SqlApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
