using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQOrder.ConsoleApp;
using System.Reflection;

// Get the directory path of the DocumentStoreManagement project
string baseDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(DocumentStoreManagement.WeatherForecast)).Location);

// Create the host builder
var hostBuilder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(baseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Configure the services
        Startup.ConfigureServices(services, hostContext.Configuration);

        // Add the consumer as a hosted service
        services.AddHostedService<OrderConsumer>();
    });

// Build the host
var host = hostBuilder.Build();

// Run the host
await host.RunAsync();
