namespace Sample
{
    public class Startup
    {
        readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
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
}