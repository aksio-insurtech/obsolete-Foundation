using System.Reflection;
using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines a system that can work with event handlers.
    /// </summary>
    public interface IEventHandlers
    {
        /// <summary>
        /// Register an event handler by its handler type and method.
        /// </summary>
        /// <param name="eventHandlerId">The unique <see cref="EventHandlerId"/> for the owning handler.</param>
        /// <param name="eventClrType">The Clr <see cref="Type"/> representing the event type.</param>
        /// <param name="eventType">The actual <see cref="EventType"/>.</param>
        /// <param name="handler">The owning handler <see cref="Type"/>.</param>
        /// <param name="method">The <see cref="MethodInfo"/> that can be invoked.</param>
        /// <returns><see cref="IEventHandler"/> registered.</returns>
        IEventHandler Register(EventHandlerId eventHandlerId, Type eventClrType, EventTypeId eventType, Type handler, MethodInfo method);
    }
}