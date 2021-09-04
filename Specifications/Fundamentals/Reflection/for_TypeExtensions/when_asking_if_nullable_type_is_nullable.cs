namespace Aksio.Reflection.for_TypeExtensions
{
    public class when_asking_if_nullable_type_is_nullable : Specification
    {
        static bool result;

        void Because() => result = typeof(int?).IsNullable();

        [Fact] void should_return_true() => result.ShouldBeTrue();
    }
}
