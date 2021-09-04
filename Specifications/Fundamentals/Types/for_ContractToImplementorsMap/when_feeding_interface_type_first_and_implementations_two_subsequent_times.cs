namespace Aksio.Types.for_ContractToImplementorsMap
{
    public class when_feeding_interface_type_first_and_implementations_two_subsequent_times : given.an_empty_map
    {
        void Establish() => map.Feed(new[] { typeof(IInterface) });

        void Because()
        {
            map.Feed(new[] { typeof(ImplementationOfInterface) });
            map.Feed(new[] { typeof(SecondImplementationOfInterface) });
        }

        [Fact] void should_have_both_the_implementations_only() => map.GetImplementorsFor(typeof(IInterface)).ShouldContainOnly(typeof(ImplementationOfInterface), typeof(SecondImplementationOfInterface));
    }
}
