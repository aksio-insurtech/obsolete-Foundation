namespace Aksio.Events.EventLogs
{
    /// <summary>
    /// Defines a system that can provide metadata to all events being appended.
    /// </summary>
    public interface ICanProvideAdditionalEventInformation
    {
        /// <summary>
        /// Provide additional information on events being appended.
        /// </summary>
        /// <param name="event">The event being appended.</param>
        /// <returns>Object containing the additional information.</returns>
        object ProvideFor(object @event);
    }
}