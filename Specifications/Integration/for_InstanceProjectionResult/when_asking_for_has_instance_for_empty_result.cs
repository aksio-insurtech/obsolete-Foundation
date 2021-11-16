using Dolittle.SDK.Events;

namespace Aksio.Integration.for_InstanceProjectionResult
{
    public class when_asking_for_has_instance_for_empty_result : Specification
    {
        static EventSourceId key = "3da63900-a667-49a5-9d91-80616c0bf091";

        InstanceProjectionResult<Model> projection_result;
        bool result;

        void Establish() => projection_result = new();

        void Because() => result = projection_result.HasInstance(key);

        [Fact] void should_not_consider_having_it() => result.ShouldBeFalse();
    }
}