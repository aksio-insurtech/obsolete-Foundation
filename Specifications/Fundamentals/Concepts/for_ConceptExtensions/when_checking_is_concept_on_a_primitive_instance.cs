namespace Aksio.Concepts.for_ConceptExtensions
{
    public class when_checking_is_concept_on_a_primitive_instance : Concepts.given.concepts
    {
        static bool is_a_concept;

        void Because() => is_a_concept = 1.IsConcept();

        [Fact] void should_not_be_a_concept() => is_a_concept.ShouldBeFalse();
    }
}
