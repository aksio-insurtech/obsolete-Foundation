using System.Reflection;
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

        readonly Dictionary<Type, IEventHandler> _eventHandlersByClrType = new();
        readonly Dictionary<EventTypeId, IEventHandler> _eventHandlersByEventType = new();

        /// <inheritdoc/>
        public IEventHandler Register(EventHandlerId eventHandlerId, Type eventClrType, EventTypeId eventType, Type handler, MethodInfo method)
        {
            var eventHandler = new MethodOnTypeEventHandler(eventHandlerId, handler, method);
            _eventHandlersByClrType[eventClrType] = eventHandler;
            _eventHandlersByEventType[eventType] = eventHandler;
            return eventHandler;
        }
    }
}