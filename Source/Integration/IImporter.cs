namespace Aksio.Integration
{
    /// <summary>
    /// Defines a system for working with import of data.
    /// </summary>
    public interface IImporter
    {
        /// <summary>
        /// Get a specific <see cref="ImportContext{TModel, TExternalModel}"/>.
        /// </summary>
        /// <typeparam name="TModel">Type of model to get for.</typeparam>
        /// <typeparam name="TExternalModel">Type of external model to get for.</typeparam>
        /// <returns><see cref="ImportContext{TModel, TExternalModel}"/>.</returns>
        IImportOperations<TModel, TExternalModel> For<TModel, TExternalModel>();
    }
}
