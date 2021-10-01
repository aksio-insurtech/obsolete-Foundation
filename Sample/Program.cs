using Sample;

var builder = Host.CreateDefaultBuilder()
                    .UseAksio()
                    .UseCratis()
                    .ConfigureWebHostDefaults(_ => _.UseStartup<Startup>());

var app = builder.Build();

app.Run();
