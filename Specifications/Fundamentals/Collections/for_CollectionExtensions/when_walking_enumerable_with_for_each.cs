namespace Aksio.Collections.for_CollectionExtensions
{
    public class when_walking_enumerable_with_for_each : Specification
    {
        IEnumerable<string> enumerable;
        string[] actual_enumerable = new[]
        {
            "first",
            "second",
            "third"
        };

        List<string> result = new();

        void Establish() => enumerable = actual_enumerable;

        void Because() => enumerable.ForEach(result.Add);

        [Fact] void should_walk_through_all_elements() => result.ShouldContainOnly(actual_enumerable);
    }
}
