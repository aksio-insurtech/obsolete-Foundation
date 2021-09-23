using Aksio.Concepts.given;

namespace Aksio.Concepts.for_ConceptMap
{
    public class when_getting_the_primitive_type_from_an_multilevel_inheritance_concept_based_on_a_long : Specification
    {
        static Type result;

        void Because() => result = ConceptMap.GetConceptValueType(typeof(MultiLevelInheritanceConcept));

        [Fact] void should_get_a_long() => result.ShouldEqual(typeof(long));
    }
}
