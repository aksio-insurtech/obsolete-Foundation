namespace Aksio.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_a_multi_level_inherited_concept : Specification
    {
        const long long_value = 42;

        InheritedConcept result;

        void Because() => result = ConceptFactory.CreateConceptInstance(typeof(MultiLevelInheritedConcept), long_value) as MultiLevelInheritedConcept;

        [Fact] void should_hold_the_correct_long_value() => result.Value.ShouldEqual(long_value);
    }
}
