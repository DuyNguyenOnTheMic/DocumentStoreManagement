using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Infrastructure.Repositories.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentStoreManagement.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL context
            services.AddScoped<DbContext, SqlApplicationContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(SqlGenericRepository<>));
            services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            var connectionString = configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
            var issuerUri = configuration["IdentityServer:IssuerUri"];
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));

            // MongoDB context
            /*builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDBDatabase"));
            builder.Services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            builder.Services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
            builder.Services.AddScoped<IDocument, DocumentBo>();*/
            return services;
        }
    }
}
