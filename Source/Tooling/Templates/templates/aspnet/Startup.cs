namespace AksioMicroserviceTemplate
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseAksio();
            app.UseRouting();
        }
    }
}