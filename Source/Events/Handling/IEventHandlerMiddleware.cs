using Dolittle.SDK.Events;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines a middleware that can be called during the handling of events.
    /// </summary>
    public interface IEventHandlerMiddleware
    {
        /// <summary>
        /// Invoke the middleware.
        /// </summary>
        /// <param name="eventContext"><see cref="EventContext"/> for the event.</param>
        /// <param name="event">The actual event that it will be called with.</param>
        /// <param name="next"><see cref="Action"/> for calling the next middleware in the chain.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task Invoke(EventContext eventContext, object @event, NextEventHandlerMiddleware next);
    }
}