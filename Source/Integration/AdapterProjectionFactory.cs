using Cratis.Concepts;
using Cratis.Events;
using Cratis.Events.Projections;
using Cratis.Events.Projections.Json;
using Cratis.Extensions.Dolittle.EventStore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IEventStore = Cratis.Extensions.Dolittle.EventStore.IEventStore;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapterProjectionFactory"/>.
    /// </summary>
    public class AdapterProjectionFactory : IAdapterProjectionFactory
    {
        readonly IEventStore _eventStore;
        readonly IEventTypes _eventTypes;
        readonly IJsonProjectionSerializer _projectionSerializer;
        readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdapterProjectionFactory"/> class.
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to use.</param>
        /// <param name="eventTypes">The <see cref="IEventTypes"/> to use.</param>
        /// <param name="projectionSerializer"><see cref="IJsonProjectionSerializer"/> used for deserialization to Cratis Kernel ProjectionDefinition.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for creating loggers.</param>
        public AdapterProjectionFactory(
            IEventStore eventStore,
            IEventTypes eventTypes,
            IJsonProjectionSerializer projectionSerializer,
            ILoggerFactory loggerFactory)
        {
            _eventStore = eventStore;
            _eventTypes = eventTypes;
            _projectionSerializer = projectionSerializer;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public IAdapterProjectionFor<TModel> CreateFor<TModel, TExternalModel>(IAdapterFor<TModel, TExternalModel> adapter)
        {
            var projectionBuilder = new ProjectionBuilderFor<TModel>(Guid.Empty, _eventTypes);
            adapter.DefineModel(projectionBuilder);
            var definition = projectionBuilder.Build();

            var converters = new JsonConverter[]
            {
                new ConceptAsJsonConverter(),
                new ConceptAsDictionaryJsonConverter()
            };

            var json = JsonConvert.SerializeObject(definition, converters);
            var parsed = _projectionSerializer.Deserialize(json);
            var projection = _projectionSerializer.CreateFrom(parsed);

            return new AdapterProjectionFor<TModel>(projection, _eventStore.GetStream(EventStreamId.EventLog), _loggerFactory);
        }
    }
}
