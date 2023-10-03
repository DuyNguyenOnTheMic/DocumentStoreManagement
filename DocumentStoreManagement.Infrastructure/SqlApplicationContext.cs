using DocumentStoreManagement.Core.Models.SQL;
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
