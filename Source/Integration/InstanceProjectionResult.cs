using System.Dynamic;
using Cratis.Changes;
using Cratis.Events.Projections;
using EventSourceId = Dolittle.SDK.Events.EventSourceId;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IProjectionStorage"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public class InstanceProjectionResult<TModel> : IProjectionStorage
    {
        readonly Dictionary<EventSourceId, TModel> _instances = new();

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

        /// <summary>
        /// Get the actual instance of the model with the <see cref="EventSourceId"/>.
        /// </summary>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> to get for.</param>
        /// <returns>The instance.</returns>
        public TModel GetInstance(EventSourceId eventSourceId)
        {
            return _instances[eventSourceId];
        }

        /// <summary>
        /// Check if there is an instance for a specific <see cref="EventSourceId"/>.
        /// </summary>
        /// <param name="eventSourceId"><see cref="EventSourceId"/>.</param>
        /// <returns>True if it exists, false if not.</returns>
        public bool HasInstance(EventSourceId eventSourceId)
        {
            return _instances.ContainsKey(eventSourceId);
        }
    }
}
