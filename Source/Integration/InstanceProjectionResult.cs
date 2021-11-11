using System.Dynamic;
using Cratis.Changes;
using Cratis.Events.Projections;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IProjectionStorage"/>.
    /// </summary>
    public class InstanceProjectionResult : IProjectionStorage
    {
        /// <inheritdoc/>
        public Task ApplyChanges(Model model, object key, Changeset<Event, ExpandoObject> changeset)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<ExpandoObject> FindOrDefault(Model model, object key)
        {
            return Task.FromResult(new ExpandoObject());
        }
    }
}
