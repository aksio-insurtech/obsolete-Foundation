namespace Aksio.Concepts.for_ConceptExtensions
{
    public class when_checking_is_concept_on_a_concept : Concepts.given.concepts
    {
        static bool is_a_concept;

        void Because() => is_a_concept = value_as_a_long.GetType().IsConcept();

        [Fact] void should_be_a_concept() => is_a_concept.ShouldBeTrue();
    }
}
