using Cratis.Changes;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents the context when translating output for an <see cref="AdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    /// <param name="CurrentState">The current state.</param>
    /// <param name="Changeset">The <see cref="Changeset{TExternalModel, TModel}"/> in the context..</param>
    /// <typeparam name="TModel">Type of model the translation is for.</typeparam>
    /// <typeparam name="TExternalModel">Type of the external model to do translation from.</typeparam>
    public record ExportContext<TModel, TExternalModel>(TModel CurrentState, Changeset<TExternalModel, TModel> Changeset);
}
