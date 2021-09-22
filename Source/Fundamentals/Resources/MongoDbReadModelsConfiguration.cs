namespace Aksio.Resources
{
    /// <summary>
    /// Represents a <see cref="ResourceConfiguration"/> for MondoDB read models.
    /// </summary>
    public class MongoDbReadModelsConfiguration : ResourceConfiguration
    {
        /// <summary>
        /// Gets the host.
        /// </summary>
        public string Host { get; init; } = string.Empty;

        /// <summary>
        /// Gets the database.
        /// </summary>
        public string Database { get; init; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether or not to use SSL.
        /// </summary>
        public bool UseSSL { get; init; }
    }
}
