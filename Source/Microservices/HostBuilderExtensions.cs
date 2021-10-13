using Aksio;
using Cratis.DependencyInversion;
using Cratis.Extensions.Dolittle.Workbench;
using Cratis.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Provides extension methods for <see cref="IHostBuilder"/>.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Use Aksio defaults with the <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
        /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
        public static IHostBuilder UseAksio(this IHostBuilder builder)
        {
            var types = new Types("Aksio");

            builder.ConfigureServices(_ =>
            {
                _
                  .AddSingleton<ITypes>(types)
                  .AddSingleton<ProviderFor<IServiceProvider>>(() => Internals.ServiceProvider!)
                  .AddControllersFromProjectReferencedAssembles(types)
                  .AddProjections()
                  .AddDolittle(types, () => Internals.ServiceProvider!)
                  .AddDolittleEventTypes()
                  .AddDolittleSchemaStore("localhost", 27017)
                  .AddDolittleProjections()
                  .AddCratisWorkbench(_ => _.UseDolittle());

                // Temporarily adding this, due to a bug in .NET 6 (https://www.ingebrigtsen.info/2021/09/29/autofac-asp-net-core-6-hot-reload-debug-crash/):
                _.AddRazorPages();
            });

            builder
                .UseDefaultConfiguration()
                .UseConfigurationObjects(types, "data")
                .UseDefaultLogging()
                .UseDefaultDependencyInversion(types)
                .UseExecutionContext();

            return builder;
        }
    }
}