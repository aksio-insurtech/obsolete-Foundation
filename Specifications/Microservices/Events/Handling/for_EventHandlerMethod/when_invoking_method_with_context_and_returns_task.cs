using System.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Events.Handling.for_EventHandlerMethod
{
    public class when_invoking_method_with_context_and_returns_task : Specification
    {
        const string event_content = "Something";

        class HandlerClass
        {
            public bool called;
            public object event_content;
            public EventContext event_context;
            public void TheHandleMethod(object @event, EventContext context)
            {
                called = true;
                event_content = @event;
                event_context = context;
            }
        }

        Mock<IServiceProvider> service_provider;
        EventHandlerMethod method;
        HandlerClass handler;
        EventContext event_context;
        Task result;

        void Establish()
        {
            service_provider = new();
            event_context = new EventContext(0, null, null, DateTimeOffset.UtcNow, null, null);
            method = new EventHandlerMethod(
                                    new EventType(Guid.NewGuid()),
                                    typeof(object),
                                    typeof(HandlerClass).GetMethod(nameof(HandlerClass.TheHandleMethod), BindingFlags.Instance | BindingFlags.Public),
                                    () => service_provider.Object);

            handler = new HandlerClass();
            service_provider.Setup(_ => _.GetService(typeof(HandlerClass))).Returns(handler);
        }

        void Because() => result = method.Invoke(event_content, event_context);

        [Fact] void should_call_handle_method() => handler.called.ShouldBeTrue();
        [Fact] void should_pass_the_correct_event_content() => handler.event_content.ShouldEqual(event_content);
        [Fact] void should_pass_the_event_context() => handler.event_context.ShouldEqual(event_context);
        [Fact] void should_return_task() => result.ShouldNotBeNull();
    }
}