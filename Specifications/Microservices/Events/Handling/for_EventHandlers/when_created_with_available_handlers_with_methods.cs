using Cratis.Types;
using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;
using IEventTypes = Aksio.Events.Types.IEventTypes;

namespace Aksio.Events.Handling.for_EventHandlers
{
    public class when_created_with_available_handlers_with_methods : Specification
    {
        [EventType("450243cb-cc02-4718-a4b4-9c31e619c589")]
        record first_event;

        [EventType("2f21bc75-9be9-43a9-8a46-ff893138ef32")]
        record second_event;

        [EventHandler("995093ff-9a77-4c14-9317-66f8d372bd4a")]
        class synchronous_handler
        {
            public void first_method(first_event @event) { }
            public void second_method(second_event @event, EventContext context) { }

            public bool invalid_signature(second_event @event) => true;
        }

        [EventHandler("ff8bdc66-108d-45ed-a998-771670ae8eac")]
        class asynchronous_handler
        {
            public Task first_method(first_event @event) => Task.CompletedTask;
            public Task second_method(second_event @event, EventContext context) => Task.CompletedTask;
            public bool invalid_signature(second_event @event) => true;
        }

        Mock<ITypes> types;
        Mock<IEventTypes> event_types;
        Mock<IServiceProvider> service_provider;
        EventHandlers event_handlers;

        void Establish()
        {
            types = new();
            event_types = new();
            service_provider = new();
            types.Setup(_ => _.All)
                .Returns(new Type[]
                {
                    typeof(synchronous_handler),
                    typeof(asynchronous_handler),
                    typeof(first_event),
                    typeof(second_event)
                });
        }

        void Because() => event_handlers = new EventHandlers(types.Object, event_types.Object, () => service_provider.Object);

        [Fact] void should_have_two_event_handlers() => event_handlers.All.Count().ShouldEqual(2);
        [Fact] void should_have_two_methods_on_first_handler() => event_handlers.All.First().Methods.Count().ShouldEqual(2);
        [Fact] void should_have_two_methods_on_second_handler() => event_handlers.All.ToArray()[1].Methods.Count().ShouldEqual(2);
    }
}