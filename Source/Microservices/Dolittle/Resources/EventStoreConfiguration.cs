namespace Aksio.Dolittle.Resources
{
    /// <summary>
    /// Represents a <see cref="ResourceConfiguration"/> for the Dolittle event store.
    /// </summary>
    /// <param name="Servers">MongoDB servers to connect to.</param>
    /// <param name="Database">Database to use.</param>
    public record EventStoreConfiguration(IEnumerable<string> Servers, string Database) : ResourceConfiguration;
}
