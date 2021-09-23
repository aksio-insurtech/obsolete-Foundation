using Aksio.Execution;
using MongoDB.Driver;

namespace Aksio.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IMongoDBClientFactory"/>.
    /// </summary>
    [Singleton]
    public class MongoDBClientFactory : IMongoDBClientFactory
    {
        /// <inheritdoc/>
        public IMongoClient Create(MongoClientSettings settings) => new MongoClient(settings);

        /// <inheritdoc/>
        public IMongoClient Create(MongoUrl url) => new MongoClient(url);

        /// <inheritdoc/>
        public IMongoClient Create(string connectionString) => new MongoClient(connectionString);
    }
}
