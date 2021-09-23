#pragma warning disable CS1718

namespace Aksio.Concepts.for_ConceptAs
{
    public class when_constructing_with_null_value : Specification
    {
        record RefConcept(string Value) : ConceptAs<string>(Value);

        Exception result;

        void Because() => result = Catch.Exception(() => { var refConcept = new RefConcept(null); });

        [Fact] void should_throw_argument_null_exception() => result.ShouldBeOfExactType<ArgumentNullException>();
    }
}
