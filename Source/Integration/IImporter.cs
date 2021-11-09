namespace Aksio.Integration
{
    /// <summary>
    /// Represents the context to work with translation of models and external models.
    /// </summary>
    public interface IImporter
    {
        /// <summary>
        /// Get a specific <see cref="ImportContext{TModel, TExternalModel}"/>.
        /// </summary>
        /// <typeparam name="TModel">Type of model to get for.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model to get for.</typeparam>
        /// <returns><see cref="ImportContext{TModel, TExternalModel}"/>.</returns>
        /// <exception cref="NotImplementedException">Not implemented.</exception>
        ImportContext<TModel, TExternalModel> For<TModel, TExternalModel>();
    }
}
