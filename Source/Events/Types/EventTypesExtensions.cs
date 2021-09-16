using Aksio.Reflection;
using Aksio.Types;
using Dolittle.SDK;
using Dolittle.SDK.Events;

namespace Aksio.Events.Types
{
    /// <summary>
    /// Provides extension methods related to event types.
    /// </summary>
    public static class EventTypesExtensions
    {
        /// <summary>
        /// Automatically discover all event types in any project referenced assemblies.
        /// </summary>
        /// <param name="clientBuilder">The Dolittle <see cref="ClientBuilder"/>.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <returns><see cref="ClientBuilder"/> for continuation.</returns>
        public static ClientBuilder WithAutoDiscoveredEventTypes(this ClientBuilder clientBuilder, ITypes types)
        {
            var eventTypes = types.All.Where(_ => _.HasAttribute<EventTypeAttribute>());
            clientBuilder.WithEventTypes(_ =>
            {
                foreach (var eventType in eventTypes)
                {
                    _.Register(eventType);
                }
            });

            return clientBuilder;
        }
    }
}