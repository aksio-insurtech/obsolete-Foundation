using System.Reflection;

namespace Aksio.Reflection.for_TypeExtensions
{
    public class when_getting_default_constructor_from_type_with_default_constructor : Specification
    {
        ConstructorInfo constructor_info;

        void Because() => constructor_info = typeof(TypeWithDefaultConstructor).GetDefaultConstructor();

        [Fact] void should_return_a_constructor() => constructor_info.ShouldNotBeNull();
    }
}
