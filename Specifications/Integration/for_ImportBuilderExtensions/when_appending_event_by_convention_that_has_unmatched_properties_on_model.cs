namespace Aksio.Integration.for_ImportBuilderExtensions
{
    public class when_appending_event_by_convention_that_has_unmatched_properties_on_model : given.changes_on_two_properties
    {
        Exception result;

        void Establish() => subject.AppendEvent<Model, ExternalModel, SomeEventWithMoreProperties>();

        void Because() => result = Catch.Exception(() => subject.OnNext(new ImportContext<Model, ExternalModel>(changeset, events_to_append)));

        [Fact] void should_throw_missing_expected_event_property_on_model() => result.ShouldBeOfExactType<MissingExpectedEventPropertyOnModel>();
    }
}