using System.Reflection;
using Cratis.Concepts;
using Cratis.Events;
using Cratis.Events.Projections;
using Cratis.Events.Projections.Json;
using Cratis.Extensions.Dolittle.EventStore;
using Cratis.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapters"/>.
    /// </summary>
    public class Adapters : IAdapters
    {
        readonly ITypes _types;
        readonly IServiceProvider _serviceProvider;
        readonly IEventStream _eventStream;
        readonly IEventTypes _eventTypes;
        readonly JsonProjectionSerializer _projectionSerializer;
        readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adapters"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> for getting instances from the IoC container.</param>
        /// <param name="eventStream"><see cref="IEventStream"/> to use.</param>
        /// <param name="eventTypes">The <see cref="IEventTypes"/> to use.</param>
        /// <param name="projectionSerializer"><see cref="JsonProjectionSerializer"/> used for deserialization to Cratis Kernel ProjectionDefinition.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for creating loggers.</param>
        public Adapters(
            ITypes types,
            IServiceProvider serviceProvider,
            IEventStream eventStream,
            IEventTypes eventTypes,
            JsonProjectionSerializer projectionSerializer,
            ILoggerFactory loggerFactory)
        {
            _types = types;
            _serviceProvider = serviceProvider;
            _eventStream = eventStream;
            _eventTypes = eventTypes;
            _projectionSerializer = projectionSerializer;
            _loggerFactory = loggerFactory;
            PopulateAdapters();
        }

        /// <inheritdoc/>
        public IAdapterFor<TModel, TExternalModel> GetFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();
            return AdaptersByKey<TModel, TExternalModel>.Adapter!;
        }

        /// <inheritdoc/>
        public IAdapterProjectionFor<TModel> GetProjectionFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();
            if (AdaptersByKey<TModel, TExternalModel>.Projection is null)
            {
                CreateProjectionFor<TModel, TExternalModel>();
            }
            return AdaptersByKey<TModel, TExternalModel>.Projection!;
        }

        void PopulateAdapters()
        {
            foreach (var adapterType in _types.FindMultiple(typeof(IAdapterFor<,>)))
            {
                var adapterInterface = adapterType.GetInterface(typeof(IAdapterFor<,>).Name)!;
                var adaptersByKey = typeof(AdaptersByKey<,>).MakeGenericType(adapterInterface.GenericTypeArguments);
                var adapterProperty = adaptersByKey.GetField(nameof(AdaptersByKey<object, object>.Adapter), BindingFlags.Public | BindingFlags.Static)!;
                var adapter = _serviceProvider.GetService(adapterType);
                adapterProperty.SetValue(null, adapter);
            }
        }

        void CreateProjectionFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();

            var projectionBuilder = new ProjectionBuilderFor<TModel>(Guid.Empty, _eventTypes);
            AdaptersByKey<TModel, TExternalModel>.Adapter!.DefineModel(projectionBuilder);
            var definition = projectionBuilder.Build();

            var converters = new JsonConverter[]
            {
                new ConceptAsJsonConverter(),
                new ConceptAsDictionaryJsonConverter()
            };

            var json = JsonConvert.SerializeObject(definition, converters);
            var parsed = _projectionSerializer.Deserialize(json);
            var projection = _projectionSerializer.CreateFrom(parsed);

            AdaptersByKey<TModel, TExternalModel>.Projection = new AdapterProjectionFor<TModel>(projection, _eventStream, _loggerFactory);
        }

        void ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>()
        {
            if (AdaptersByKey<TModel, TExternalModel>.Adapter is null)
            {
                throw new MissingAdapterForModelAndExternalModel(typeof(TModel), typeof(TExternalModel));
            }
        }

        static class AdaptersByKey<TModel, TExternalModel>
        {
            #pragma warning disable CS0649 // We're assigning using reflection
            public static IAdapterFor<TModel, TExternalModel>? Adapter;
            public static IAdapterProjectionFor<TModel>? Projection;
        }
    }
}
