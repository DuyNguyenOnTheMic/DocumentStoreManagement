using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Infrastructure.Repositories.Mongo;
using DocumentStoreManagement.Services;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            /*services.AddScoped<DbContext, SqlApplicationContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(SqlGenericRepository<>));
            services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            var connectionString = configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
            var issuerUri = configuration["IdentityServer:IssuerUri"];
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));*/

            // MongoDB context
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
            return services;
        }
    }
}
