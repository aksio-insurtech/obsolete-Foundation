namespace Aksio.Types.for_ContractToImplementorsMap
{
    public class when_getting_implementors_of_abstract_class_that_has_two_implementations : given.an_empty_map
    {
        IEnumerable<Type> result;

        void Establish() => map.Feed(new[] { typeof(ImplementationOfAbstractClass), typeof(SecondImplementationOfAbstractClass) });

        void Because() => result = map.GetImplementorsFor(typeof(AbstractClass));

        [Fact] void should_have_both_the_implementations_only() => result.ShouldContainOnly(typeof(ImplementationOfAbstractClass), typeof(SecondImplementationOfAbstractClass));
    }
}
