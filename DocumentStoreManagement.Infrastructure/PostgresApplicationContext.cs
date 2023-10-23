using DocumentStoreManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Infrastructure
{
    public class PostgresApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
