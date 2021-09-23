﻿namespace Aksio.Concepts.for_ConceptExtensions
{
    public class when_getting_the_value_from_a_non_concept : given.concepts
    {
        static string primitive_value = "ten";
        static Exception exception;

        void Because() => exception = Catch.Exception(() => primitive_value.GetConceptValue());

        [Fact] void should_throw_an_argument_exception() => exception.ShouldBeOfExactType<TypeIsNotAConcept>();
    }
}
