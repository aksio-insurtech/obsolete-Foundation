using Dolittle.SDK.Artifacts;
using Dolittle.SDK.Events;

namespace Events.Schemas
{
    /// <summary>
    /// Defines the store for event schemas.
    /// </summary>
    public interface ISchemaStore
    {
        /// <summary>
        /// Generate a schema for a specific type.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to define from.</param>
        /// <returns><see cref="EventSchema"/> for the type.</returns>
        EventSchema GenerateFor(Type type);

        /// <summary>
        /// Save an <see cref="EventSchema"/> to the store.
        /// </summary>
        /// <param name="schema"><see cref="EventSchema"/> to save.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Save(EventSchema schema);

        /// <summary>
        /// Check if an <see cref="EventSchema"/> for a specific <see cref="EventType"/> exists.
        /// </summary>
        /// <param name="type"><see cref="EventType"/> to check for.</param>
        /// <param name="generation">Optional <see cref="Generation"/>.</param>
        /// <returns>True if there is a schema for the type, false if not.</returns>
        /// <remarks>
        /// If generation is not provided, it will get what is associated with the <see cref="EventType"/>.
        /// </remarks>
        Task<bool> HasFor(EventType type, Generation? generation = null);

        /// <summary>
        /// Gets a <see cref="EventSchema"/> for a specific <see cref="Type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get for.</param>
        /// <param name="generation">Optional <see cref="Generation"/>.</param>
        /// <returns><see cref="EventSchema"/> for the type.</returns>
        /// <remarks>
        /// If generation is not provided, it will get what is associated with the <see cref="EventType"/>.
        /// </remarks>
        Task<EventSchema> GetFor(EventType type, Generation? generation = null);
    }
}