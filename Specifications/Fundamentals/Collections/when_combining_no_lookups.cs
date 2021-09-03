
namespace Aksio.Collections.for_CollectionExtensions
{
    public class when_combining_no_lookups : Specification
    {
        static IEnumerable<ILookup<string, int>> lookups;
        static ILookup<string, int> result;

        void Establish() => lookups = Enumerable.Empty<ILookup<string, int>>();

        void Because() => result = lookups.Combine();

        [Fact] void it_should_be_empty() => result.Count.ShouldEqual(0);
    }
}
