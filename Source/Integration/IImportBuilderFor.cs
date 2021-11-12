namespace Aksio.Integration
{
    /// <summary>
    /// Defines a builder for building how to handle input changes.
    /// </summary>
    /// <typeparam name="TModel">Model to build for.</typeparam>
    /// <typeparam name="TExternalModel">The type of the external model.</typeparam>
    public interface IImportBuilderFor<TModel, TExternalModel> : IObservable<ImportContext<TModel, TExternalModel>>
    {
    }
}