using Dolittle.SDK.Events;

namespace Aksio.Integration.for_ImportOperations
{
    public class when_applying_external_model_with_no_changes : given.no_changes
    {
        async Task Because() => await operations.Apply(incoming);

        [Fact] void should_not_append_any_events() => event_log.Verify(_ => _.Append(IsAny<EventSourceId>(), IsAny<object>()), Never());
    }
}