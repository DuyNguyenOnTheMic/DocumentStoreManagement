using DocumentStoreManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Infrastructure
{
    /// <summary>
    /// Sql Application Db Context
    /// </summary>
    /// <param name="options"></param>
    public class SqlApplicationContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Student Table
        /// </summary>
        public DbSet<Student> Students { get; set; }
    }
}
