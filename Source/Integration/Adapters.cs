using System.Reflection;
using AutoMapper;
using Cratis.Concepts;
using Cratis.Events;
using Cratis.Events.Projections;
using Cratis.Events.Projections.Json;
using Cratis.Extensions.Dolittle.EventStore;
using Cratis.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IEventStore = Cratis.Extensions.Dolittle.EventStore.IEventStore;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapters"/>.
    /// </summary>
    public class Adapters : IAdapters
    {
        readonly ITypes _types;
        readonly IServiceProvider _serviceProvider;
        readonly IEventStore _eventStore;
        readonly IEventTypes _eventTypes;
        readonly JsonProjectionSerializer _projectionSerializer;
        readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adapters"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> for getting instances from the IoC container.</param>
        /// <param name="eventStore"><see cref="IEventStream"/> to use.</param>
        /// <param name="eventTypes">The <see cref="IEventTypes"/> to use.</param>
        /// <param name="projectionSerializer"><see cref="JsonProjectionSerializer"/> used for deserialization to Cratis Kernel ProjectionDefinition.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for creating loggers.</param>
        public Adapters(
            ITypes types,
            IServiceProvider serviceProvider,
            IEventStore eventStore,
            IEventTypes eventTypes,
            JsonProjectionSerializer projectionSerializer,
            ILoggerFactory loggerFactory)
        {
            _types = types;
            _serviceProvider = serviceProvider;
            _eventStore = eventStore;
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

        /// <inheritdoc/>
        public IMapper GetMapperFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();
            if (AdaptersByKey<TModel, TExternalModel>.Mapper is null)
            {
                CreateMapperFor<TModel, TExternalModel>();
            }
            return AdaptersByKey<TModel, TExternalModel>.Mapper!;
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

            AdaptersByKey<TModel, TExternalModel>.Projection = new AdapterProjectionFor<TModel>(projection, _eventStore.GetStream(EventStreamId.EventLog), _loggerFactory);
        }

        void CreateMapperFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.ShouldUseConstructor = ci =>
                {
                    var parameters = ci.GetParameters();
                    return !ci.IsPrivate && !(parameters.Length == 1 && parameters[0].ParameterType.Equals(ci.DeclaringType));
                };
                cfg.ShouldMapMethod = mi => false;
                cfg.ShouldMapField = fi => !fi.IsPrivate;
                cfg.AllowNullDestinationValues = true;
                var mapping = cfg.CreateMap<TExternalModel, TModel>();
                mapping = mapping.DisableCtorValidation();
                AdaptersByKey<TModel, TExternalModel>.Adapter!.DefineImportMapping(mapping!);
            });
            AdaptersByKey<TModel, TExternalModel>.Mapper = configuration.CreateMapper();
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
            public static IMapper? Mapper;
        }
    }
}
