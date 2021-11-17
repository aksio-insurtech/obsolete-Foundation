using System.Dynamic;
using Cratis.Changes;
using Cratis.Events.Projections;
using EventSourceId = Dolittle.SDK.Events.EventSourceId;

namespace Aksio.Integration.for_InstanceProjectionResult
{
    public class when_applying_properties_changed : Specification
    {
        static EventSourceId key = "3da63900-a667-49a5-9d91-80616c0bf091";

        InstanceProjectionResult<Model> projection_result;
        Changeset<Event, ExpandoObject> changeset;

        void Establish()
        {
            projection_result = new();
            changeset = new Changeset<Event, ExpandoObject>(null!, new ExpandoObject());
            IDictionary<string, object> incoming = new ExpandoObject();
            incoming[nameof(Model.SomeInteger)] = 42;
            incoming[nameof(Model.SomeString)] = "Forty Two";

            changeset.Add(new PropertiesChanged<ExpandoObject>(incoming as ExpandoObject, Array.Empty<PropertyDifference<ExpandoObject>>()));
        }

        async Task Because() => await projection_result.ApplyChanges(null, key, changeset);

        [Fact] void should_consider_having_the_instance() => projection_result.HasInstance(key).ShouldBeTrue();
        [Fact] void should_hold_the_instance() => projection_result.GetInstance(key).ShouldNotBeNull();
        [Fact] void should_hold_the_instance_with_integer_value() => projection_result.GetInstance(key).SomeInteger.ShouldEqual(42);
        [Fact] void should_hold_the_instance_with_string_value() => projection_result.GetInstance(key).SomeString.ShouldEqual("Forty Two");
    }
}