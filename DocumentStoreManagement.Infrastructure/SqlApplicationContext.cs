using DocumentStoreManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Infrastructure
{
    public class SqlApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
    }
}
