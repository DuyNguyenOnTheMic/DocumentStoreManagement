using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Infrastructure.Repositories.SQL;
using DocumentStoreManagement.Services;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentStoreManagement.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Add the dependency injections of database and repositories here
        /// </summary>
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL context
            services.AddScoped<DbContext, PostgresApplicationContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(SqlGenericRepository<>));
            //services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            //var connectionString = configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
            //var issuerUri = configuration["IdentityServer:IssuerUri"];
            //services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));

            // MongoDB context
            /*services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));*/

            // Postgres context
            services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddDbContext<PostgresApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
            return services;
        }
    }
}
