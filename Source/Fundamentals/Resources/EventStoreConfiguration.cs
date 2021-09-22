namespace Aksio.Resources
{
    /// <summary>
    /// Represents a <see cref="ResourceConfiguration"/> for the event store.
    /// </summary>
    public class EventStoreConfiguration : ResourceConfiguration
    {
        /// <summary>
        /// Gets the MongoDB servers to connect to.
        /// </summary>
        public string[] Servers { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Gets the name of the database to use.
        /// </summary>
        public string Database { get; init; } = string.Empty;
    }
}
