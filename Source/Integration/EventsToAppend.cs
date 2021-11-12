using System.Collections;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents the translated events from <see cref="AdapterFor{TModel, TExternalModel}"/>.
    /// </summary>
    public class EventsToAppend : IEnumerable, IEnumerable<object>
    {
        readonly List<object> _events = new();

        /// <summary>
        /// Add an event.
        /// </summary>
        /// <param name="event">Event to add.</param>
        public void Add(object @event)
        {
            _events.Add(@event);
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator() => _events.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator<object> IEnumerable<object>.GetEnumerator() => _events.GetEnumerator();
    }
}
