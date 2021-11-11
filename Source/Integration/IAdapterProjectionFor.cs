using Dolittle.SDK.Events;

namespace Aksio.Integration
{
    /// <summary>
    /// Defines a projection for an adapter for a specific model.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IAdapterProjectionFor<TModel>
    {
        /// <summary>
        /// Get an instance by <see cref="EventSourceId"/>.
        /// </summary>
        /// <param name="eventSourceId">The <see cref="EventSourceId"/> to get for.</param>
        /// <returns>Instance of the model.</returns>
        TModel GetById(EventSourceId eventSourceId);
    }
}
