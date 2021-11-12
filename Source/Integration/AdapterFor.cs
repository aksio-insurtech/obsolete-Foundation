using System.Linq.Expressions;
using AutoMapper;
using Cratis.Events.Projections;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Model the adapter is for.</typeparam>
    /// <typeparam name="TExternalModel">Type in the external system.</typeparam>
    public abstract class AdapterFor<TModel, TExternalModel> : IAdapterFor<TModel, TExternalModel>
    {
        /// <inheritdoc/>
        public abstract Expression<Func<TExternalModel, global::Dolittle.SDK.Events.EventSourceId>> Key { get; }

        /// <summary>
        /// Define the model / state based on projection from events.
        /// </summary>
        /// <param name="builder"><see cref="IProjectionBuilderFor{TModel}"/>.</param>
        public virtual void DefineModel(IProjectionBuilderFor<TModel> builder)
        {
        }

        /// <summary>
        /// Define the translation of incoming data to zero or more events.
        /// </summary>
        /// <param name="builder"><see cref="IImportBuilderFor{TModel, TExternalModel}"/> for building what to do when input changes occur.</param>
        public virtual void DefineImport(IImportBuilderFor<TModel, TExternalModel> builder)
        {
        }

        /// <summary>
        /// Define the mapping between an input model and the model used in the adapter.
        /// </summary>
        /// <param name="builder">The <see cref="IMappingExpression{TExternalSystem, TModel}"/> to use.</param>
        public virtual void DefineImportMapping(IMappingExpression<TExternalModel, TModel> builder)
        {
        }
    }
}