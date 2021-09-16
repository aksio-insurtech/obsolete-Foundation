using Dolittle.SDK.Events;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines an interface for a callback that will be called before any of the event handlers.
    /// </summary>
    public interface ICanBeCalledBeforeEventHandlerInvoked
    {
        /// <summary>
        /// Called before every handle method is invoked on an event handler.
        /// </summary>
        /// <param name="eventHandler"><see cref="IEventHandler"/> that will be invoked.</param>
        /// <param name="event">The actual event that it will be called with.</param>
        /// <param name="eventContext"><see cref="EventContext"/> for the event.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task BeforeInvoked(IEventHandler eventHandler, object @event, EventContext eventContext);
    }
}