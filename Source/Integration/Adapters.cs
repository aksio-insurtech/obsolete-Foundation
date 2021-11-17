using System.Reflection;
using AutoMapper;
using Cratis.Types;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapters"/>.
    /// </summary>
    public class Adapters : IAdapters
    {
        readonly ITypes _types;
        readonly IServiceProvider _serviceProvider;
        readonly IAdapterProjectionFactory _adapterProjectionFactory;
        readonly IAdapterMapperFactory _adapterMapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adapters"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> for getting instances from the IoC container.</param>
        /// <param name="adapterProjectionFactory"><see cref="IAdapterProjectionFactory"/> for creating projections for adapters.</param>
        /// <param name="adapterMapperFactory"><see cref="IAdapterMapperFactory"/> for creating mappers for adapters.</param>
        public Adapters(
            ITypes types,
            IServiceProvider serviceProvider,
            IAdapterProjectionFactory adapterProjectionFactory,
            IAdapterMapperFactory adapterMapperFactory)
        {
            _types = types;
            _serviceProvider = serviceProvider;
            _adapterProjectionFactory = adapterProjectionFactory;
            _adapterMapperFactory = adapterMapperFactory;
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
                AdaptersByKey<TModel, TExternalModel>.Projection = _adapterProjectionFactory.CreateFor(AdaptersByKey<TModel, TExternalModel>.Adapter!);
            }
            return AdaptersByKey<TModel, TExternalModel>.Projection!;
        }

        /// <inheritdoc/>
        public IMapper GetMapperFor<TModel, TExternalModel>()
        {
            ThrowIfMissingAdapterForModelAndExternalModel<TModel, TExternalModel>();
            if (AdaptersByKey<TModel, TExternalModel>.Mapper is null)
            {
                AdaptersByKey<TModel, TExternalModel>.Mapper = _adapterMapperFactory.CreateFor(AdaptersByKey<TModel, TExternalModel>.Adapter!);
            }
            return AdaptersByKey<TModel, TExternalModel>.Mapper!;
        }

        void PopulateAdapters()
        {
            var adaptersByKeyType = typeof(AdaptersByKey<,>);

            foreach (var adapterType in _types.FindMultiple(typeof(IAdapterFor<,>)))
            {
                var adapterInterface = adapterType.GetInterface(typeof(IAdapterFor<,>).Name)!;
                var adaptersByKey = adaptersByKeyType.MakeGenericType(adapterInterface.GenericTypeArguments);
                var adapterProperty = adaptersByKey.GetField(nameof(AdaptersByKey<object, object>.Adapter), BindingFlags.Public | BindingFlags.Static)!;
                var adapter = _serviceProvider.GetService(adapterType);
                adapterProperty.SetValue(null, adapter);
            }
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
