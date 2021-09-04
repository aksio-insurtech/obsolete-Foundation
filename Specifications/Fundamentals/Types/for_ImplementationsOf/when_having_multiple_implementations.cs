namespace Aksio.Types.for_ImplementationsOf
{
    public class when_having_multiple_implementations : Specification
    {
        Mock<ITypes> type_finder_mock;
        Type[] instances;

        void Establish()
        {
            type_finder_mock = new();
            type_finder_mock.Setup(t => t.FindMultiple<IAmAnInterface>()).Returns(new Type[]
            {
                typeof(OneImplementation),
                typeof(SecondImplementation)
            });
        }

        void Because() => instances = new ImplementationsOf<IAmAnInterface>(type_finder_mock.Object).ToArray();

        [Fact] void should_get_the_implementations() => instances.ShouldContainOnly(new[] { typeof(OneImplementation), typeof(SecondImplementation) });
    }
}
