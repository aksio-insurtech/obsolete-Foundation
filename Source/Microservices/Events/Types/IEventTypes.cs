using Dolittle.SDK.Events;

namespace Aksio.Events.Types
{
    /// <summary>
    /// Represents a system for managing <see cref="IEventTypes"/>.
    /// </summary>
    public interface IEventTypes
    {
        /// <summary>
        /// Check a particular <see cref="Type"/> is registered.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check for.</param>
        /// <returns>True if it exists, false if not.</returns>
        bool HasFor(Type type);

        /// <summary>
        /// Get the <see cref="EventType"/> for a particular <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get for.</param>
        /// <returns>The <see cref="EventType"/>.</returns>
        EventType GetFor(Type type);
    }
}