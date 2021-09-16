using System.Reflection;
using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandler"/> for event handlers that are methods on types.
    /// </summary>
    public class MethodOnTypeEventHandler : IEventHandler
    {
        readonly Type _type;
        readonly MethodInfo _methodInfo;
        readonly bool _needsContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodOnTypeEventHandler"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the handler.</param>
        /// <param name="type"><see cref="Type"/> that holds method.</param>
        /// <param name="methodInfo">The actual <see cref="MethodInfo"/>.</param>
        public MethodOnTypeEventHandler(EventHandlerId id, Type type, MethodInfo methodInfo)
        {
            Id = id;
            _type = type;
            _methodInfo = methodInfo;
            _needsContext = methodInfo.GetParameters().Length == 2;
        }

        /// <inheritdoc/>
        public EventHandlerId Id { get; }

        /// <inheritdoc/>
        public Task Invoke(object @event, EventContext context)
        {
            object returnValue;
            var handlerInstance = EventHandlers.ServiceProvider?.GetService(_type) ?? Activator.CreateInstance(_type);
            if (_needsContext)
            {
                returnValue = _methodInfo.Invoke(handlerInstance, new object[] { @event, context })!;
            }
            else
            {
                returnValue = _methodInfo.Invoke(handlerInstance, new object[] { @event })!;
            }

            if (returnValue is Task task) return task;

            return Task.CompletedTask;
        }
    }
}