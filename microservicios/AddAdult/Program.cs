using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AddAdult;
using AddAdult.Data;

IServiceCollection serviceDescriptors = new ServiceCollection();

Host.CreateDefaultBuilder(args)
   .ConfigureAppConfiguration(configHost =>
   {
       // Configure just the environment variables to use with container apps.
       configHost.AddEnvironmentVariables();

       //    // Use with docker compose up --build 
       //    configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
   })
   .ConfigureServices((hostContext, services) =>
   {
       IConfiguration configuration = hostContext.Configuration;
       var connectionString = configuration.GetConnectionString("DefaultConnection")
                              ?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");

       services.AddOptions();
       services.AddHostedService<Worker>();
       services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(connectionString));
   }).Build().Run();