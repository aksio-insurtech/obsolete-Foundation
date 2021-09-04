using System.Collections;

namespace Aksio.Reflection.for_TypeExtensions.for_IsEnumerable
{
    public class MyEnumerable : IEnumerable<ComplexType>
    {
        IEnumerable<ComplexType> _list = new List<ComplexType>();

        public IEnumerator<ComplexType> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}