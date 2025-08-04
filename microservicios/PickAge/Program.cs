using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickAge;

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
    IConfiguration configuration = hostContext.Configuration;
    services.AddOptions();
    services.AddHostedService<Worker>();
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString));
});

// Ejecutar la aplicación
hostBuilder.Build().Run();
