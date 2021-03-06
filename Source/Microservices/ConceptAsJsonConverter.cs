using System.Text.Json;
using System.Text.Json.Serialization;
using Cratis.Concepts;

namespace Aksio
{
    /// <summary>
    /// Represents a <see cref="JsonConverter{T}"/> for <see cref="ConceptAs{T}"/>.
    /// </summary>
    /// <typeparam name="T">Underlying concept type.</typeparam>
    public class ConceptAsJsonConverter<T> : JsonConverter<T>
    {
        /// <inheritdoc/>
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => (T)ConceptFactory.CreateConceptInstance(typeToConvert, reader.GetString())!;

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => writer.WriteStringValue(value?.GetConceptValue().ToString());
    }
}
