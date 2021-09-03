
namespace Aksio.BDD
{
    public static class AssertionExtensions
    {
        public static void ShouldEqual(this object input, object expected)
        {
            Assert.Equal(input, expected);
        }
    }
}
