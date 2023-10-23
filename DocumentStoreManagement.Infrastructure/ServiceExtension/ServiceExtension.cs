using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Infrastructure.Repositories.SQL;
using DocumentStoreManagement.Services;
using DocumentStoreManagement.Services.Commands.DocumentCommands;
using DocumentStoreManagement.Services.Handlers.DocumentHandlers;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.MessageBroker;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(SqlGenericRepository<>));
            var connectionString = configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));*/

            // MongoDB context
            /*services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDBDatabase"));
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            services.AddTransient<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
            string connectionString = configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));*/

            // Postgres context
            services.AddScoped<DbContext, PostgresApplicationContext>();
            services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(SqlQueryRepository<>));
            services.AddDbContext<DbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
            services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDBDatabase"));
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Register services
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IOrderService, OrderService>();

            // Register RabbitMQ
            services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

            // Register generic request handler of MediatR
            services.AddTransient<IRequestHandler<CreateDocumentCommand<Book>, Book>, CreateDocumentHandler<Book>>();
            services.AddTransient<IRequestHandler<CreateDocumentCommand<Magazine>, Magazine>, CreateDocumentHandler<Magazine>>();
            services.AddTransient<IRequestHandler<CreateDocumentCommand<Newspaper>, Newspaper>, CreateDocumentHandler<Newspaper>>();

            return services;
        }
    }
}
