using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Infrastructure;
using DocumentStoreManagement.Infrastructure.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RabbitMQOrder.ConsoleApp
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register your dependencies here
            services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDBDatabase"));
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
        }
    }
}