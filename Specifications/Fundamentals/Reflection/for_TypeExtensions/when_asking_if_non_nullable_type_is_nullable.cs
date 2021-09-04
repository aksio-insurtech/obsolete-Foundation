namespace Aksio.Reflection.for_TypeExtensions
{
    public class when_asking_if_non_nullable_type_is_nullable : Specification
    {
        static bool result;

        void Because() => result = typeof(int).IsNullable();

        [Fact] void should_return_false() => result.ShouldBeFalse();
    }
}
