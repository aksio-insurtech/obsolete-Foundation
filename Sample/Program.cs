using Autofac.Extensions.DependencyInjection;
using Serilog;

var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
      .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = Host.CreateDefaultBuilder()
                    .UseSerilog()
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureWebHostDefaults(_ => _
                        .UseStartup<Startup>())
                    ;
var app = builder.Build();

app.Run();
