using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Core.Models.SQL
{
    public class SqlApplicationContext : DbContext
    {
        public SqlApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
