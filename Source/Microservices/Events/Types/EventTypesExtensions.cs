using Dolittle.SDK;

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
        /// <param name="eventTypes">The <see cref="IEventTypes"/>.</param>
        /// <returns><see cref="ClientBuilder"/> for continuation.</returns>
        public static ClientBuilder WithAutoDiscoveredEventTypes(this ClientBuilder clientBuilder, IEventTypes eventTypes)
        {
            clientBuilder.WithEventTypes(_ =>
            {
                foreach (var eventType in eventTypes.TypeMap.Keys)
                {
                    _.Register(eventType);
                }
            });

            return clientBuilder;
        }
    }
}