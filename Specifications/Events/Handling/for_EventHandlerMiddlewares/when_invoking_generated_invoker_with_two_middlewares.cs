using System.Reflection;
using Aksio.Types;
using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling.Builder;

namespace Aksio.Events.Handling
{
    public class when_invoking_generated_invoker_with_two_middlewares : Specification
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

        Mock<IEventHandlerMiddleware> first_middleware;
        Mock<IEventHandlerMiddleware> second_middleware;
        Mock<IServiceProvider> service_provider;
        Mock<ITypes> types;

        EventHandlerMethod method;

        HandlerClass handler;
        EventContext event_context;

        EventHandlerMiddlewares middlewares;
        TaskEventHandlerSignature invoker;

        void Establish()
        {
            first_middleware = new();
            second_middleware = new();
            service_provider = new();

            event_context = new EventContext(0, null, null, DateTimeOffset.UtcNow, null, null);
            method = new EventHandlerMethod(
                                    new EventType(Guid.NewGuid()),
                                    typeof(object),
                                    typeof(HandlerClass).GetMethod(nameof(HandlerClass.TheHandleMethod), BindingFlags.Instance | BindingFlags.Public),
                                    () => service_provider.Object);

            handler = new HandlerClass();

            types = new();
            types.Setup(_ => _.FindMultiple<IEventHandlerMiddleware>()).Returns(new Type[]
            {
                first_middleware.GetType(),
                second_middleware.GetType()
            });
            service_provider.Setup(_ => _.GetService(typeof(HandlerClass))).Returns(handler);
            service_provider.SetupSequence(_ => _.GetService(first_middleware.GetType()))
                .Returns(first_middleware.Object)
                .Returns(second_middleware.Object);

            first_middleware.Setup(_ => _.Invoke(IsAny<EventContext>(), IsAny<object>(), IsAny<NextEventHandlerMiddleware>())).Callback(
                (EventContext eventContext, object @event, NextEventHandlerMiddleware next) => next());

            second_middleware.Setup(_ => _.Invoke(IsAny<EventContext>(), IsAny<object>(), IsAny<NextEventHandlerMiddleware>())).Callback(
                (EventContext eventContext, object @event, NextEventHandlerMiddleware next) => next());

            middlewares = new EventHandlerMiddlewares(types.Object, () => service_provider.Object);
            invoker = middlewares.CreateInvokerFor(method);
        }

        void Because() => invoker.Invoke(event_content, event_context);

        [Fact] void should_call_handle_method() => handler.called.ShouldBeTrue();
        [Fact] void should_pass_the_correct_event_content() => handler.event_content.ShouldEqual(event_content);
        [Fact] void should_pass_the_event_context() => handler.event_context.ShouldEqual(event_context);
        [Fact] void should_call_first_middleware() => first_middleware.Verify(_ => _.Invoke(event_context, event_content, IsAny<NextEventHandlerMiddleware>()), Once);
        [Fact] void should_call_second_middleware() => second_middleware.Verify(_ => _.Invoke(event_context, event_content, IsAny<NextEventHandlerMiddleware>()), Once);
    }
}