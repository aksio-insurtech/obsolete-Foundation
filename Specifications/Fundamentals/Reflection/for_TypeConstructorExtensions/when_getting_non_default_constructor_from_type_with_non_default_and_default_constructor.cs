using System.Reflection;

namespace Aksio.Reflection.for_TypeExtensions
{
    public class when_getting_non_default_constructor_from_type_with_non_default_and_default_constructor : Specification
    {
        ConstructorInfo constructor_info;

        void Because() => constructor_info = typeof(TypeWithDefaultAndNonDefaultConstructor).GetNonDefaultConstructor();

        [Fact] void should_return_a_constructor() => constructor_info.ShouldNotBeNull();
        [Fact] void should_return_correct_constructor() => constructor_info.GetParameters()[0].Name.ShouldEqual("something");
    }
}
