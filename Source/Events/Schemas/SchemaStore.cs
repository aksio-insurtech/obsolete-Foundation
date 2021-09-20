using System.Reflection;
using Dolittle.SDK.Artifacts;
using Dolittle.SDK.Events;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace Events.Schemas
{
    /// <summary>
    /// Represents an implementation of <see cref="ISchemaStore"/>.
    /// </summary>
    public class SchemaStore : ISchemaStore
    {
        static readonly JSchemaGenerator _generator;

        static SchemaStore()
        {
            _generator = new JSchemaGenerator
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            _generator.GenerationProviders.Add(new StringEnumGenerationProvider());
        }

        /// <inheritdoc/>
        public EventSchema GenerateFor(Type type)
        {
            TypeIsMissingEventType.ThrowIfMissingEventType(type);
            var eventTypeAttribute = type.GetCustomAttribute<EventTypeAttribute>()!;

            var schema = _generator.Generate(type);
            dynamic extension = new JObject();
            extension.pii = true;

            foreach ((var key, var value) in schema.Properties)
            {
                value.ExtensionData["gdpr"] = extension;
            }

            return new EventSchema(eventTypeAttribute.EventType, schema);
        }

        /// <inheritdoc/>
        public Task<EventSchema> GetFor(EventType type, Generation? generation = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<bool> HasFor(EventType type, Generation? generation = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task Save(EventSchema schema) => throw new NotImplementedException();
    }
}