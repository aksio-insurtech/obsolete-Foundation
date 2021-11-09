using Cratis.Changes;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents the context when translating output for an <see cref="AdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    /// <param name="CurrentState">The current state.</param>
    /// <param name="Changeset">The <see cref="Changeset{T}"/> in the context..</param>
    /// <typeparam name="TModel">Type of model the translation is for.</typeparam>
    public record AdapterExportContext<TModel>(TModel CurrentState, Changeset<TModel> Changeset);
}
