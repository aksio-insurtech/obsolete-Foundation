using Aksio.Concepts;
using MongoDB.Bson.Serialization;

namespace Aksio.MongoDB
{
    /// <summary>
    /// Represents a <see cref="IBsonSerializationProvider"/> for concepts.
    /// </summary>
    public class ConceptSerializationProvider : IBsonSerializationProvider
    {
        /// <summary>
        /// Creates an instance of a serializer of the concept of the given type param T.
        /// </summary>
        /// <typeparam name="T">The Concept type.</typeparam>
        /// <returns><see cref="ConceptSerializer{T}"/> for the specific type.</returns>
        public static ConceptSerializer<T> CreateConceptSerializer<T>()
        {
            return new ConceptSerializer<T>();
        }

        /// <inheritdoc/>
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.IsConcept())
            {
                var createConceptSerializerGenericMethod = GetType().GetMethod(nameof(ConceptSerializationProvider.CreateConceptSerializer))!.MakeGenericMethod(type);
                return (dynamic)createConceptSerializerGenericMethod.Invoke(null, Array.Empty<object>())!;
            }

            return BsonSerializer.LookupSerializer(type);
        }
    }
}