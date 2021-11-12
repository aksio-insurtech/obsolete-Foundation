using System.Dynamic;
using Cratis.Changes;
using Cratis.Concepts;
using Cratis.Dynamic;
using Cratis.Events.Projections;
using Newtonsoft.Json;
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
        public async Task ApplyChanges(Model model, object key, Changeset<Event, ExpandoObject> changeset)
        {
            var eventSourceId = new EventSourceId { Value = key.ToString() };
            var existing = await FindOrDefault(model, key);
            foreach (var change in changeset.Changes)
            {
                switch (change)
                {
                    case PropertiesChanged<ExpandoObject> propertiesChanged:
                        {
                            existing = propertiesChanged.State as ExpandoObject;
                        }
                        break;
                }
            }

            var converters = new JsonConverter[]
            {
                new ConceptAsJsonConverter(),
                new ConceptAsDictionaryJsonConverter()
            };
            var json = JsonConvert.SerializeObject(existing, converters);
            _instances[eventSourceId] = JsonConvert.DeserializeObject<TModel>(json, converters)!;
        }

        /// <inheritdoc/>
        public Task<ExpandoObject> FindOrDefault(Model model, object key)
        {
            var eventSourceId = new EventSourceId { Value = key.ToString() };
            if (_instances.ContainsKey(eventSourceId))
            {
                return Task.FromResult(_instances[eventSourceId]!.AsExpandoObject());
            }

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
