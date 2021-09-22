using Autofac;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Aksio.MongoDB
{
    /// <summary>
    /// Represents a <see cref="Module"/> for setting up defaults and bindings for MongoDB.
    /// </summary>
    public class MongoDBModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            BsonSerializer
                .RegisterSerializationProvider(
                    new ConceptSerializationProvider());
            BsonSerializer
                .RegisterSerializer(
                    new DateTimeOffsetSupportingBsonDateTimeSerializer());

            BsonSerializer
                .RegisterSerializer(
                    new GuidSerializer(GuidRepresentation.Standard));

            var conventionPack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("Aksio Conventions", conventionPack, _ => true);
        }
    }
}
