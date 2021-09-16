using Aksio.Events.Types;
using Aksio.Types;
using Dolittle.SDK;
using Dolittle.SDK.Events.Handling;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for configuring Dolittle with a specific <see cref="IServiceCollection"/>.
    /// </summary>
    public static class DolittleServiceCollectionExtensions
    {
        /// <summary>
        /// Add Dolittle to a given <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add to.</param>
        /// <param name="types"><see cref="ITypes"/> for discovery.</param>
        /// <returns><see cref="IServiceCollection"/> for continuation.</returns>
        public static IServiceCollection AddDolittle(this IServiceCollection services, ITypes types)
        {
            var clientBuilder = Client
                .ForMicroservice(Guid.Empty)
                .WithLogging(new LoggerFactory().AddSerilog(Log.Logger))
                .WithAutoDiscoveredEventHandlers(services, types)
                .WithAutoDiscoveredEventTypes(types);

            var client = clientBuilder.Build();
            services.AddSingleton(client);

            return services;
        }
    }
}