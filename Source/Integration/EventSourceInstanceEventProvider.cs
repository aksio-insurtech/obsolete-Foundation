using System.Dynamic;
using System.Reactive.Subjects;
using Cratis.Events.Projections;
using Cratis.Extensions.Dolittle.EventStore;
using MongoDB.Bson.Serialization;
using Event = Cratis.Events.Projections.Event;
using EventSourceId = Dolittle.SDK.Events.EventSourceId;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents a <see cref="IProjectionEventProvider"/> for providing events for a specific <see cref="EventSourceId"/>.
    /// </summary>
    public class EventSourceInstanceEventProvider : IProjectionEventProvider
    {
        readonly IEventStream _eventStream;
        readonly EventSourceId _eventSourceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourceInstanceEventProvider"/> class.
        /// </summary>
        /// <param name="eventStream"><see cref="IEventStream"/> to provide from.</param>
        /// <param name="eventSourceId"><see cref="EventSourceId"/> to get for.</param>
        public EventSourceInstanceEventProvider(IEventStream eventStream, EventSourceId eventSourceId)
        {
            _eventStream = eventStream;
            _eventSourceId = eventSourceId;
        }

        /// <inheritdoc/>
        public IObservable<Event> ProvideFor(IProjection projection)
        {
            var subject = new ReplaySubject<Event>();
            Task.Run(() => CatchUp(projection, subject)).Wait();
            return subject;
        }

        /// <inheritdoc/>
        public void Pause(IProjection projection)
        {
        }

        /// <inheritdoc/>
        public void Resume(IProjection projection)
        {
        }

        /// <inheritdoc/>
        public Task Rewind(IProjection projection) => Task.CompletedTask;

        async Task CatchUp(IProjection projection, ReplaySubject<Event> subject)
        {
            var eventTypes = projection.EventTypes.Select(_ => new global::Dolittle.SDK.Events.EventType(Guid.Parse(_.Value))).ToArray();

            var exhausted = false;
            var offset = 0U;

            while (!exhausted)
            {
                var cursor = await _eventStream.GetFromPosition(offset, eventTypes, _eventSourceId);
                while (await cursor.MoveNextAsync())
                {
                    if (!cursor.Current.Any())
                    {
                        exhausted = true;
                        break;
                    }

                    offset = OnNext(projection, subject, cursor.Current);
                }
            }
        }

        uint OnNext(IProjection projection, ReplaySubject<Event> subject, IEnumerable<Cratis.Extensions.Dolittle.EventStore.Event> events)
        {
            var lastSavedPosition = 0U;
            foreach (var @event in events)
            {
                var eventType = new EventType(@event.Metadata.TypeId.ToString());
                if (projection.EventTypes.Any(_ => _ == eventType))
                {
                    var content = BsonSerializer.Deserialize<ExpandoObject>(@event.Content);
                    subject.OnNext(
                        new Event(
                            @event.Id,
                            eventType,
                            @event.Metadata.Occurred,
                            @event.Metadata.EventSource,
                            content));
                }

                lastSavedPosition = @event.Id + 1;
            }

            return lastSavedPosition;
        }
    }
}
