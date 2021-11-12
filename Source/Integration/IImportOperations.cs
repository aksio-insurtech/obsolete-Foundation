namespace Aksio.Integration
{
    /// <summary>
    /// Defines import operations that can be performed.
    /// </summary>
    /// <typeparam name="TModel">Type of model the operations are for.</typeparam>
    /// <typeparam name="TExternalModel">Type of external model the operations are for.</typeparam>
    public interface IImportOperations<TModel, TExternalModel> : IDisposable
    {
        /// <summary>
        /// Apply an instance of the external model.
        /// </summary>
        /// <param name="instance">The external model instance.</param>
        /// <remarks>
        /// Objects will be mapped to the model and compared for changes and then run through
        /// the translation of changes to events.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Apply(TExternalModel instance);

        /// <summary>
        /// Apply instances of the external model.
        /// </summary>
        /// <param name="instances">The external model instances.</param>
        /// <remarks>
        /// Objects will be mapped to the model and compared for changes and then run through
        /// the translation of changes to events.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Apply(IEnumerable<TExternalModel> instances);
    }
}