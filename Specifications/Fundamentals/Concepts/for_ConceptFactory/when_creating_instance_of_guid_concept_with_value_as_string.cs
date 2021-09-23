﻿namespace Aksio.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_guid_concept_with_value_as_string : Specification
    {
        const string guid_value_as_string = "4AB92720-3138-4A7B-A7E9-2A49F6624736";

        GuidConcept result;

        void Because() => result = ConceptFactory.CreateConceptInstance(typeof(GuidConcept), guid_value_as_string) as GuidConcept;

        [Fact] void should_hold_the_correct_guid_value() => result.Value.ToString().ToUpperInvariant().ShouldEqual(guid_value_as_string);
    }
}
