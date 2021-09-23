namespace Aksio.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_datetime_concept_with_value_as_datetime : Specification
    {
        DateTimeConcept result;
        DateTime now;

        void Establish() => now = DateTime.Now;

        void Because() => result = ConceptFactory.CreateConceptInstance(typeof(DateTimeConcept), now) as DateTimeConcept;

        [Fact] void should_be_the_value_of_the_datetime() => result.Value.ShouldEqual(now);
    }
}
