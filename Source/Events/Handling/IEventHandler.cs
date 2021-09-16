using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines an event handler.
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Gets the unique identifier for the <see cref="IEventHandler"/>.
        /// </summary>
        EventHandlerId Id { get; }

        /// <summary>
        /// Invoke the event handler.
        /// </summary>
        /// <param name="event">The actual event instance.</param>
        /// <param name="context">The <see cref="EventContext">context</see> for the event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Invoke(object @event, EventContext context);
    }
}