namespace Aksio.Integration
{
    /// <summary>
    /// Defines the system for working with <see cref="AdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    public interface IAdapters
    {
        /// <summary>
        /// Gets an <see cref="AdapterFor{TModel, TExternalModel}"/> for the specific model and external model.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model.</typeparam>
        /// <returns>The <see cref="AdapterFor{TModel, TExternalModel}"/>.</returns>
        AdapterFor<TModel, TExternalModel> GetFor<TModel, TExternalModel>();
    }
}
