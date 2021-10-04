using System.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Events.Handling.for_EventHandlerMethod
{
    public class when_invoking_method_without_context_and_returns_void : Specification
    {
        const string event_content = "Something";

        class HandlerClass
        {
            public bool called;
            public object event_content;
            public void TheHandleMethod(object @event)
            {
                called = true;
                event_content = @event;
            }
        }

        Mock<IServiceProvider> service_provider;
        EventHandlerMethod method;
        HandlerClass handler;

        void Establish()
        {
            service_provider = new();
            method = new EventHandlerMethod(
                                    new EventType(Guid.NewGuid()),
                                    typeof(object),
                                    typeof(HandlerClass).GetMethod(nameof(HandlerClass.TheHandleMethod), BindingFlags.Instance | BindingFlags.Public),
                                    () => service_provider.Object);

            handler = new HandlerClass();
            service_provider.Setup(_ => _.GetService(typeof(HandlerClass))).Returns(handler);
        }

        void Because() => method.Invoke(event_content, new EventContext(0, null, null, DateTimeOffset.UtcNow, null, null));

        [Fact] void should_call_handle_method() => handler.called.ShouldBeTrue();
        [Fact] void should_pass_the_correct_event_content() => handler.event_content.ShouldEqual(event_content);
    }
}