using Aksio.Concepts.given;

namespace Aksio.Concepts.for_ConceptExtensions
{
    public class when_getting_the_value_from_an_inherited_concept : concepts
    {
        static InheritingFromLongConcept value;
        static long primitive_value = 100;
        static object returned_value;

        void Establish() => value = new InheritingFromLongConcept(primitive_value);

        void Because() => returned_value = value.GetConceptValue();

        [Fact] void should_get_the_value_of_the_primitive() => returned_value.ShouldEqual(primitive_value);
    }
}
