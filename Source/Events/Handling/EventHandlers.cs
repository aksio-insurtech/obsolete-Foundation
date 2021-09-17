using System.Reflection;
using Aksio.Reflection;
using Aksio.Types;
using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlers"/>.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> to use.
        /// </summary>
        public static IServiceProvider? ServiceProvider;
        readonly ITypes _types;
        readonly List<EventHandler> _eventHandlers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        public EventHandlers(ITypes types)
        {
            _types = types;
            Populate();
        }

        /// <inheritdoc/>
        public IEnumerable<EventHandler> All => _eventHandlers;

        void Populate()
        {
            var handlers = _types.All.Where(_ => _.HasAttribute<EventHandlerAttribute>());
            var eventTypes = _types.All.Where(_ => _.HasAttribute<EventTypeAttribute>()).ToDictionary(
                _ => _,
                _ => _.GetCustomAttribute<EventTypeAttribute>()!.EventType);

            foreach (var handler in handlers)
            {
                var methodsByEventTypeId = handler.GetHandleMethods(eventTypes);
                var eventHandler = handler.GetCustomAttribute<EventHandlerAttribute>()!;
                var eventHandlerMethods = methodsByEventTypeId.Select(_ => new EventHandlerMethod(eventTypes[_.Key], _.Key, _.Value));
                _eventHandlers.Add(new EventHandler(eventHandler.Identifier, eventHandler.Partitioned, eventHandler.Scope, handler, eventHandlerMethods));
            }
        }
    }
}