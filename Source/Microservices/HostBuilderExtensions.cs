using Aksio.Microservices;
using Cratis.DependencyInversion;
using Cratis.Extensions.Dolittle;
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

            builder.ConfigureServices(_ => _
                .AddSingleton<ITypes>(types)
                .AddSingleton<ProviderFor<IServiceProvider>>(() => Internals.ServiceProvider!)
                .AddDolittle(types, () => Internals.ServiceProvider!)
                .AddDolittleSchemaStore("localhost", 27017)
                .AddCratisWorkbench(_ => _.UseDolittle()));

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