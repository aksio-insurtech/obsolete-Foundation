using Aksio.Types;
using Autofac;

public class Startup
{
    readonly ITypes _types;
    readonly IWebHostEnvironment _environment;

    public Startup(IWebHostEnvironment environment)
    {
        _environment = environment;
        _types = new Types();
    }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterDefaults(_types);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpLogging();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapGet("/", () => "Hello World!"));
    }
}