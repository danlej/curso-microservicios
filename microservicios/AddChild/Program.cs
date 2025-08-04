using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AddAdult;
using AddAdult.Data;

// Crear host manualmente sin usar CreateDefaultBuilder
var hostBuilder = new HostBuilder();

// Configurar solo variables de entorno
hostBuilder.ConfigureAppConfiguration((hostContext, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddEnvironmentVariables();
});

// Configurar servicios
hostBuilder.ConfigureServices((hostContext, services) =>
{
    var connectionString = hostContext.GetConnectionString("DefaultConnection")
                           ?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");

    services.AddOptions();
    services.AddHostedService<Worker>();
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString));
});

// Ejecutar la aplicación
hostBuilder.Build().Run();