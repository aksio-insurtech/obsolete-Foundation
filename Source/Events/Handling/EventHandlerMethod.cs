using System.Reflection;
using Aksio.DependencyInversion;
using Dolittle.SDK.Events;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an encapsulation of a method that can handle an event.
    /// </summary>
    public class EventHandlerMethod
    {
        readonly bool _needsContext;
        readonly ProviderFor<IServiceProvider> _serviceProviderProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMethod"/> class.
        /// </summary>
        /// <param name="eventType">The <see cref="EventType"/> for the method.</param>
        /// <param name="clrEventType">The CLR <see cref="Type"/> representing the event type.</param>
        /// <param name="method">The actual <see cref="MethodInfo"/>.</param>
        /// <param name="serviceProviderProvider">Provider for providing <see cref="IServiceProvider"/>.</param>
        public EventHandlerMethod(EventType eventType, Type clrEventType, MethodInfo method, ProviderFor<IServiceProvider> serviceProviderProvider)
        {
            EventType = eventType;
            ClrEventType = clrEventType;
            Method = method;
            _serviceProviderProvider = serviceProviderProvider;
            _needsContext = method.GetParameters().Length == 2;
        }

        /// <summary>
        /// Gets the <see cref="EventType"/>.
        /// </summary>
        public EventType EventType { get; }

        /// <summary>
        /// Gets the CLR <see cref="Type"/> representation.
        /// </summary>
        public Type ClrEventType { get; }

        /// <summary>
        /// Gets the actual <see cref="MethodInfo">method</see>.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Invoke the event handler method.
        /// </summary>
        /// <param name="event">Event instance.</param>
        /// <param name="context"><see cref="EventContext"/> for the event.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task Invoke(object @event, EventContext context)
        {
            object returnValue;
            var handlerInstance = _serviceProviderProvider()?.GetService(Method.DeclaringType!) ?? Activator.CreateInstance(Method.DeclaringType!);
            if (_needsContext)
            {
                returnValue = Method.Invoke(handlerInstance, new object[] { @event, context })!;
            }
            else
            {
                returnValue = Method.Invoke(handlerInstance, new object[] { @event })!;
            }

            if (returnValue is Task task) return task;

            return Task.CompletedTask;
        }
    }
}