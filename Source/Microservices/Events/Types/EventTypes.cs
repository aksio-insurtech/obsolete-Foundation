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
        readonly IDictionary<Type, EventType> _typesToEventTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypes"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        public EventTypes(ITypes types)
        {
            _typesToEventTypes = types.All.Where(_ => _.HasAttribute<EventTypeAttribute>()).ToDictionary(_ => _, _ => _.GetCustomAttribute<EventTypeAttribute>()!.EventType);
        }

        /// <inheritdoc/>
        public EventType GetFor(Type type) => _typesToEventTypes[type];

        /// <inheritdoc/>
        public bool HasFor(Type type) => _typesToEventTypes.ContainsKey(type);
    }
}