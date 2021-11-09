using Cratis.Changes;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents the context when translating input for an <see cref="AdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    /// <param name="Changeset">The <see cref="Changeset{T}"/> in the context..</param>
    /// <param name="Events">Any <see cref="EventsToAppend"/>.</param>
    /// <typeparam name="TModel">Type of model the translation is for.</typeparam>
    /// <typeparam name="TExternalModel">Type of the external model to do translation from.</typeparam>
    public record ImportContext<TModel, TExternalModel>(Changeset<TModel> Changeset, EventsToAppend Events);
}
