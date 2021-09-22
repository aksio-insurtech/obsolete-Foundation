using System.Reflection;
using Aksio.Strings;
using Aksio.Types;
using Dolittle.SDK.Artifacts;
using Dolittle.SDK.Events;
using Newtonsoft.Json.Schema;
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
        readonly IDictionary<Type, ICanExtendSchemaForType> _schemaInformationForTypesProviders;

        static SchemaStore()
        {
            _generator = new JSchemaGenerator
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            _generator.GenerationProviders.Add(new StringEnumGenerationProvider());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaStore"/> class.
        /// </summary>
        /// <param name="schemaInformationForTypesProviders"><see cref="IInstancesOf{T}"/> of <see cref="ICanExtendSchemaForType"/>.</param>
        public SchemaStore(IInstancesOf<ICanExtendSchemaForType> schemaInformationForTypesProviders)
        {
            _schemaInformationForTypesProviders = schemaInformationForTypesProviders.ToDictionary(_ => _.Type, _ => _);
        }

        /// <inheritdoc/>
        public EventSchema GenerateFor(Type type)
        {
            TypeIsMissingEventType.ThrowIfMissingEventType(type);
            var eventTypeAttribute = type.GetCustomAttribute<EventTypeAttribute>()!;

            var schema = _generator.Generate(type);
            ExtendSchema(type, schema);

            /*
            dynamic extension = new JObject();
            extension.pii = true;

            foreach ((var key, var value) in schema.Properties)
            {
                value.Ref
                value.ExtensionData["gdpr"] = extension;
            }
            */

            return new EventSchema(eventTypeAttribute.EventType, schema);
        }

        /// <inheritdoc/>
        public Task<EventSchema> GetFor(EventType type, Generation? generation = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<bool> HasFor(EventType type, Generation? generation = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task Save(EventSchema schema) => throw new NotImplementedException();

        void ExtendSchema(Type type, JSchema schema)
        {
            foreach (var provider in _schemaInformationForTypesProviders.Where(_ => _.Key == type).Select(_ => _.Value))
            {
                provider.Extend(schema);

                foreach ((var property, var propertySchema) in schema.Properties)
                {
                    var propertyName = property.ToPascalCase();
                    var propertyInfo = type.GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        ExtendSchema(propertyInfo.PropertyType, propertySchema);
                    }
                }
            }
        }
    }
}