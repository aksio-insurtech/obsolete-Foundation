using System.Reflection;
using Cratis.DependencyInversion;
using Cratis.Reflection;
using Cratis.Types;
using Dolittle.SDK.Events.Handling;
using IEventTypes = Aksio.Events.Types.IEventTypes;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlers"/>.
    /// </summary>
    public class EventHandlers : IEventHandlers
    {
        readonly ITypes _types;
        readonly IEventTypes _eventTypes;
        readonly ProviderFor<IServiceProvider> _serviceProviderProvider;
        readonly List<EventHandler> _eventHandlers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="eventTypes">All <see cref="IEventTypes"/>.</param>
        /// <param name="serviceProviderProvider">Provider for providing <see cref="IServiceProvider"/>.</param>
        public EventHandlers(ITypes types, IEventTypes eventTypes, ProviderFor<IServiceProvider> serviceProviderProvider)
        {
            _types = types;
            _eventTypes = eventTypes;
            _serviceProviderProvider = serviceProviderProvider;
            Populate();
        }

        /// <inheritdoc/>
        public IEnumerable<EventHandler> All => _eventHandlers;

        void Populate()
        {
            foreach (var handler in _types.All.Where(_ => _.HasAttribute<EventHandlerAttribute>()))
            {
                var methodsByEventTypeId = handler.GetHandleMethods(_eventTypes.TypeMap);
                var eventHandler = handler.GetCustomAttribute<EventHandlerAttribute>()!;
                var eventHandlerMethods = methodsByEventTypeId.Select(_ => new EventHandlerMethod(_eventTypes.TypeMap[_.Key], _.Key, _.Value, _serviceProviderProvider));
                _eventHandlers.Add(new EventHandler(eventHandler.Identifier, eventHandler.Partitioned, eventHandler.Scope, handler, eventHandlerMethods));
            }
        }
    }
}