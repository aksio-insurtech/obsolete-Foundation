namespace Aksio.Integration.for_EventsToAppend
{
    public class when_adding : Specification
    {
        EventsToAppend events;
        string @event;

        void Establish()
        {
            events = new();
            @event = "Forty Two";
        }

        void Because() => events.Add(@event);

        [Fact] void should_hold_the_added_event() => events.First().ShouldEqual(@event);
    }
}