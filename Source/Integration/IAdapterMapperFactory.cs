using AutoMapper;

namespace Aksio.Integration
{
    /// <summary>
    /// Defines a factory for creating <see cref="IMapper"/> instances.
    /// </summary>
    public interface IAdapterMapperFactory
    {
        /// <summary>
        /// Create a <see cref="IMapper"/> from an <see cref="IAdapterFor{TModel, TExternalModel}"/>.
        /// </summary>
        /// <param name="adapter"><see cref="IAdapterFor{TModel, TExternalModel}"/> to create for.</param>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model.</typeparam>
        /// <returns>A new instance of <see cref="IMapper"/>.</returns>
        IMapper CreateFor<TModel, TExternalModel>(IAdapterFor<TModel, TExternalModel> adapter);
    }
}
