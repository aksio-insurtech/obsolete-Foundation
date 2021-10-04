using Main;

var builder = Host.CreateDefaultBuilder()
                    .UseAksio()
                    .ConfigureWebHostDefaults(_ => _.UseStartup<Startup>());

var app = builder.Build();

app.Run();
