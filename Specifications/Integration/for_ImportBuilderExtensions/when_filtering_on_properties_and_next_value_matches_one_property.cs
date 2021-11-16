using System.Reactive.Subjects;
using Cratis.Changes;
using ObjectsComparer;

namespace Aksio.Integration.for_ImportBuilderExtensions
{
    public class when_filtering_on_properties_and_next_value_matches_one_property : given.a_change_on_one_property
    {
        ImportContext<Model, ExternalModel> result;

        void Establish()
        {
            context = import_builder.WithProperties(_ => _.SomeString);
            context.Subscribe(_ => result = _);
        }

        void Because() => subject.OnNext(new ImportContext<Model, ExternalModel>(changeset, events_to_append));

        [Fact] void should_filter_through_the_context() => result.ShouldNotBeNull();
    }
}