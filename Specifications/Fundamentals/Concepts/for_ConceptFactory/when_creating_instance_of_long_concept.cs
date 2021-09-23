namespace Aksio.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_long_concept : Specification
    {
        const long long_value = 42;

        LongConcept result;

        void Because() => result = ConceptFactory.CreateConceptInstance(typeof(LongConcept), long_value) as LongConcept;

        [Fact] void should_hold_the_correct_long_value() => result.Value.ShouldEqual(long_value);
    }
}
