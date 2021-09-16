using Dolittle.SDK;
using Dolittle.SDK.Tenancy;

namespace Sample
{

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseAksio();

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapGet("/", () =>
            {
                var @event = new
                {
                    Blah = 42
                };

                var client = app.ApplicationServices.GetService<Client>();

                client.EventStore.ForTenant(TenantId.Development).Commit(_ => _
                    .CreateEvent(@event)
                    .FromEventSource(Guid.NewGuid())
                    .WithEventType(Guid.Parse("aa4e1a9e-c481-4a2a-abe8-4799a6bbe3b7"), 1)).Wait();
            }));
        }
    }
}