using Cratis.Events.Projections;
using Cratis.Extensions.Dolittle.EventStore;
using MongoDB.Bson;
using MongoDB.Driver;
using Event = Cratis.Extensions.Dolittle.EventStore.Event;
using EventSourceId = Dolittle.SDK.Events.EventSourceId;

namespace Aksio.Integration.for_EventSourceInstanceEventProvider
{
    public class when_providing_for_specific_instance_with_two_events : Specification
    {
        static EventSourceId key = "2d1fd92f-1025-482f-b3be-ab530a2abfc7";
        static EventType first_event_type = new("417fbb4e-b0ed-4825-bc2d-13aa9c86b1b2");
        static EventType second_event_type = new("2c5fe6cc-1078-446d-8e03-ff05e1c03661");

        Mock<IEventStream> event_stream;
        Mock<IProjection> projection;
        EventSourceInstanceEventProvider provider;

        Event first_event;
        Event second_event;

        List<Cratis.Events.Projections.Event> results;

        void Establish()
        {
            event_stream = new();
            projection = new();
            provider = new(event_stream.Object, key);

            var eventTypes = new[] { first_event_type, second_event_type };
            projection.SetupGet(_ => _.EventTypes).Returns(eventTypes);

            first_event = new(
                0,
                new(Guid.NewGuid(), Guid.Empty, Guid.Empty, new(0, 0, 0, 0, string.Empty), string.Empty, Array.Empty<Claim>()),
                new(DateTimeOffset.UtcNow, string.Empty, Guid.Parse(first_event_type.Value), 0, false),
                new(false, Guid.Empty, 0, 0),
                new(false, 0, DateTimeOffset.UtcNow, Guid.Empty),
                new { Something = 42 }.ToBsonDocument());

            second_event = new(
                1,
                new(Guid.NewGuid(), Guid.Empty, Guid.Empty, new(0, 0, 0, 0, string.Empty), string.Empty, Array.Empty<Claim>()),
                new(DateTimeOffset.UtcNow, string.Empty, Guid.Parse(second_event_type.Value), 0, false),
                new(false, Guid.Empty, 0, 0),
                new(false, 0, DateTimeOffset.UtcNow, Guid.Empty),
                new { Something = 43 }.ToBsonDocument());

            var cursor = new Mock<IAsyncCursor<Event>>();

            IEnumerable<Event> resultToReturn = Array.Empty<Event>();

            cursor.SetupSequence(_ => _.MoveNextAsync(IsAny<CancellationToken>()))
                    .ReturnsAsync(() =>
                    {
                        resultToReturn = new[] { first_event };
                        return true;
                    })
                    .ReturnsAsync(() =>
                    {
                        resultToReturn = new[] { second_event };
                        return true;
                    })
                    .ReturnsAsync(() =>
                    {
                        resultToReturn = Array.Empty<Event>();
                        return false;
                    });

            cursor.SetupGet(_ => _.Current).Returns(() => resultToReturn);

            event_stream
                    .Setup(_ => _.GetFromPosition(0, IsAny<IEnumerable<global::Dolittle.SDK.Events.EventType>>(), key))
                    .ReturnsAsync(cursor.Object);

            var endOfStreamCursor = new Mock<IAsyncCursor<Event>>();
            endOfStreamCursor.SetupGet(_ => _.Current).Returns(Array.Empty<Event>());
            endOfStreamCursor.Setup(_ => _.MoveNextAsync(IsAny<CancellationToken>())).ReturnsAsync(false);

            event_stream
                    .Setup(_ => _.GetFromPosition(2, IsAny<IEnumerable<global::Dolittle.SDK.Events.EventType>>(), key))
                    .ReturnsAsync(cursor.Object);

            var observable = provider.ProvideFor(projection.Object);
            results = new();
            observable.Subscribe(results.Add);
        }

        [Fact] void should_provide_two_events() => results.Count.ShouldEqual(2);
        [Fact] void should_provide_first_event_with_correct_sequence_number() => results[0].SequenceNumber.ShouldEqual((EventLogSequenceNumber)0);
        [Fact] void should_provide_second_event_with_correct_sequence_number() => results[1].SequenceNumber.ShouldEqual((EventLogSequenceNumber)1);
        [Fact] void should_provide_first_event_with_correct_event_type() => results[0].Type.ShouldEqual(first_event_type);
        [Fact] void should_provide_second_event_with_correct_event_type() => results[1].Type.ShouldEqual(second_event_type);
        [Fact] void should_provide_first_event_with_correct_occurred() => results[0].Occurred.ShouldEqual(first_event.Metadata.Occurred);
        [Fact] void should_provide_second_event_with_correct_occurred() => results[1].Occurred.ShouldEqual(second_event.Metadata.Occurred);
        [Fact] void should_provide_first_event_with_correct_event_source() => results[0].EventSourceId.Value.ShouldEqual(first_event.Metadata.EventSource);
        [Fact] void should_provide_second_event_with_correct_event_source() => results[1].EventSourceId.Value.ShouldEqual(second_event.Metadata.EventSource);
        [Fact] void should_provide_first_event_with_correct_content() => ((int)((dynamic)results[0].Content).Something).ShouldEqual(42);
        [Fact] void should_provide_second_event_with_correct_content() => ((int)((dynamic)results[1].Content).Something).ShouldEqual(43);
    }
}