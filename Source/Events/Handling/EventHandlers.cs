using System.Reflection;
using Aksio.DependencyInversion;
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
        readonly ITypes _types;
        readonly ProviderFor<IServiceProvider> _serviceProviderProvider;
        readonly List<EventHandler> _eventHandlers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="serviceProviderProvider">Provider for providing <see cref="IServiceProvider"/>.</param>
        public EventHandlers(ITypes types, ProviderFor<IServiceProvider> serviceProviderProvider)
        {
            _types = types;
            _serviceProviderProvider = serviceProviderProvider;
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
                var eventHandlerMethods = methodsByEventTypeId.Select(_ => new EventHandlerMethod(eventTypes[_.Key], _.Key, _.Value, _serviceProviderProvider));
                _eventHandlers.Add(new EventHandler(eventHandler.Identifier, eventHandler.Partitioned, eventHandler.Scope, handler, eventHandlerMethods));
            }
        }
    }
}