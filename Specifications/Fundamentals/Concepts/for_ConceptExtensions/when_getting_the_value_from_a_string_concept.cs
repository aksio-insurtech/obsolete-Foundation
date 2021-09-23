using Aksio.Concepts.given;

namespace Aksio.Concepts.for_ConceptExtensions
{
    public class when_getting_the_value_from_a_string_concept : concepts
    {
        static StringConcept value;
        static string primitive_value = "ten";
        static object returned_value;

        void Establish() => value = new StringConcept(primitive_value);

        void Because() => returned_value = value.GetConceptValue();

        [Fact] void should_get_the_value_of_the_primitive() => returned_value.ShouldEqual(primitive_value);
    }
}
