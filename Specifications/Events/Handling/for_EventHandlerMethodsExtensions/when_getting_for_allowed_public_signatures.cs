using System.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Events.Handling.for_EventHandlerMethodsExtensions
{
    public class when_getting_for_allowed_public_signatures : Specification
    {
        record FirstEvent();
        record SecondEvent();
        record ThirdEvent();
        record ForthEvent();

        class handler
        {
            public Task FirstHappened(FirstEvent @event) => Task.CompletedTask;
            public void ForTheSecond(SecondEvent @event) {}
            public Task ThirdTimeIsACharm(ThirdEvent @event, EventContext context) => Task.CompletedTask;
            public void MayTheForth(ForthEvent @event, EventContext context) {}
        }

        IDictionary<Type, MethodInfo> methods;

        void Because() => methods = typeof(handler).GetHandleMethods(new Dictionary<Type, EventTypeId> {
            { typeof(FirstEvent), Guid.NewGuid() },
            { typeof(SecondEvent), Guid.NewGuid() },
            { typeof(ThirdEvent), Guid.NewGuid() },
            { typeof(ForthEvent), Guid.NewGuid() }
        });

        [Fact] void should_contain_first_handler() => methods.Values.Count(_ => _.Name == "FirstHappened").ShouldEqual(1);
        [Fact] void should_contain_second_handler() => methods.Values.Count(_ => _.Name == "ForTheSecond").ShouldEqual(1);
        [Fact] void should_contain_third_handler() => methods.Values.Count(_ => _.Name == "ThirdTimeIsACharm").ShouldEqual(1);
        [Fact] void should_contain_forth_handler() => methods.Values.Count(_ => _.Name == "MayTheForth").ShouldEqual(1);
    }
}