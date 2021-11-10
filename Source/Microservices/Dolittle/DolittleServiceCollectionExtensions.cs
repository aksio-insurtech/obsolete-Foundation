using Aksio.Events.Types;
using Cratis.Concepts;
using Cratis.DependencyInversion;
using Cratis.Types;
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
        /// <param name="serviceProviderProvider">Provider for providing <see cref="IServiceProvider"/>.</param>
        /// <returns><see cref="IServiceCollection"/> for continuation.</returns>
        public static IServiceCollection AddDolittle(this IServiceCollection services, ITypes types, ProviderFor<IServiceProvider> serviceProviderProvider)
        {
            var eventTypes = new EventTypes(types);

            var clientBuilder = Client
                .ForMicroservice(Guid.Empty)
#pragma warning disable CA2000 // Requirement of disposing all references before exiting scope.
                .WithLogging(new LoggerFactory().AddSerilog(Log.Logger))
#pragma warning restore
                .WithAutoDiscoveredEventHandlers(services, types, eventTypes, serviceProviderProvider)
                .WithAutoDiscoveredEventTypes(eventTypes)
                .WithEventSerializerSettings(_ =>
                {
                    _.Converters.Add(new ConceptAsJsonConverter());
                    _.Converters.Add(new ConceptAsDictionaryJsonConverter());
                });

            var client = clientBuilder.Build();
            services.AddSingleton(client);

            return services;
        }
    }
}