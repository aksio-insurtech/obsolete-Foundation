using Dolittle.SDK.Events;

namespace Aksio.Events.EventLogs
{
    /// <summary>
    /// Defines an event log.
    /// </summary>
    public interface IEventLog
    {
        /// <summary>
        /// Commit an event to the log.
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> the event belongs to.</param>
        /// <param name="event">Event to commit.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Append(EventSourceId eventSourceId, object @event);
    }
}