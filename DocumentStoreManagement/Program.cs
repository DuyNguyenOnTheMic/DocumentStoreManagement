using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models.MongoDB;
using DocumentStoreManagement.Infrastructure.Repositories.Mongo;
using DocumentStoreManagement.Services;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// SQL context
/*builder.Services.AddScoped<DbContext, SqlApplicationContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(SqlGenericRepository<>));
builder.Services.AddTransient<IUnitOfWork, SqlUnitOfWork>();
var connectionString = builder.Configuration.GetConnectionString("SqlDbConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbConnection' not found.");
var issuerUri = builder.Configuration["IdentityServer:IssuerUri"];
builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));*/

// MongoDB context
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDBDatabase"));
builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
builder.Services.AddScoped<IDocument, DocumentBo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
