using Aksio.Concepts.given;

namespace Aksio.Concepts.for_ConceptMap
{
    public class when_getting_the_primitive_type_from_a_string_concept : Specification
    {
        static Type result;

        void Because() => result = ConceptMap.GetConceptValueType(typeof(StringConcept));

        [Fact] void should_get_a_string() => result.ShouldEqual(typeof(string));
    }
}
