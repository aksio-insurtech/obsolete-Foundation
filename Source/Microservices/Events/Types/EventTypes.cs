using System.Reflection;
using Cratis.Execution;
using Cratis.Reflection;
using Cratis.Types;
using Dolittle.SDK.Events;

namespace Aksio.Events.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventTypes"/>.
    /// </summary>
    [Singleton]
    public class EventTypes : IEventTypes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypes"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        public EventTypes(ITypes types)
        {
            TypeMap = types.All.Where(_ => _.HasAttribute<EventTypeAttribute>()).ToDictionary(_ => _, _ =>
            {
                var eventTypeAttribute = _.GetCustomAttribute<EventTypeAttribute>()!;
                return new EventType(eventTypeAttribute.Identifier, eventTypeAttribute.Generation, eventTypeAttribute.Alias);
            });
        }

        /// <inheritdoc/>
        public IDictionary<Type, EventType> TypeMap { get; }

        /// <inheritdoc/>
        public EventType GetFor(Type type) => TypeMap[type];

        /// <inheritdoc/>
        public bool HasFor(Type type) => TypeMap.ContainsKey(type);
    }
}