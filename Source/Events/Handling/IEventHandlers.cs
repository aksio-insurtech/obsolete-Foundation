namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines a system that can work with event handlers.
    /// </summary>
    public interface IEventHandlers
    {
        /// <summary>
        /// Gets all registered <see cref="EventHandler">event handlers</see>.
        /// </summary>
        IEnumerable<EventHandler> All { get; }
    }
}