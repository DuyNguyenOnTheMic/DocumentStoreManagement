using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Infrastructure;
using DocumentStoreManagement.Infrastructure.Repositories.Mongo;
using DocumentStoreManagement.Services;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDBDatabase"));
builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddScoped<IMongoApplicationContext, MongoApplicationContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(MongoGenericRepository<>));
builder.Services.AddScoped<IDocumentService, DocumentService>();
//builder.Services.AddDIServices(builder.Configuration);

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
