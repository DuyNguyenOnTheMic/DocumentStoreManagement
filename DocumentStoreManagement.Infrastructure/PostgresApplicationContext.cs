using DocumentStoreManagement.Core.Models.MongoDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DocumentStoreManagement.Infrastructure
{
    public class PostgresApplicationContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PostgresApplicationContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Document> Documents { get; set; }
    }
}
