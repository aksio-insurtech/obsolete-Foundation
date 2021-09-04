namespace Aksio.Reflection.for_TypeExtensions
{
    public class when_asking_type_if_implements_generic_interface_without_specifying_generic_arguments : Specification
    {
        static bool result;

        void Because() => result = typeof(ClassImplementingGenericInterface).HasInterface(typeof(IInterfaceWithGenericArguments<>));

        [Fact] void should_have_the_interface() => result.ShouldBeTrue();
    }
}
