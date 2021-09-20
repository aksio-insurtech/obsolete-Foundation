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
        /// <param name="event">Event to commit.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Commit(object @event);
    }
}