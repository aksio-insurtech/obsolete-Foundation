using Sample;
using Aksio.Types;

var types = new Types();

var builder = Host.CreateDefaultBuilder()
                    .UseDefaultConfiguration()
                    .UseDefaultLogging()
                    .UseDefaultDependencyInversion(types)
                    .ConfigureWebHostDefaults(_ => _.UseStartup<Startup>());
var app = builder.Build();

app.Run();
