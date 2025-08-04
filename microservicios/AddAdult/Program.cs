using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AddAdult;
using AddAdult.Data;

IServiceCollection serviceDescriptors = new ServiceCollection();

Host.CreateDefaultBuilder(args)
   .ConfigureHostConfiguration(configHost =>
   {
       configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
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