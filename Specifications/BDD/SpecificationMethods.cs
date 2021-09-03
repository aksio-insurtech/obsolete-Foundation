
using System.Reflection;

namespace Aksio.BDD
{
    public class SpecificationMethods<T>
    {
        static IEnumerable<MethodInfo> _establish { get; }
        static IEnumerable<MethodInfo> _because { get; }
        static IEnumerable<MethodInfo> _destroy { get; }

        static SpecificationMethods()
        {
            _establish = GetMethodsFor("Establish");
            _destroy = GetMethodsFor("Destroy");
            _because = GetMethodsFor("Because");
        }

        static IEnumerable<MethodInfo> GetMethodsFor(string name)
        {
            var type = typeof(T);
            var methods = new List<MethodInfo>();

            while (type != typeof(Specification))
            {
                var method = type.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (method != null) methods.Insert(0, method);
                type = type.BaseType;
            }

            return methods;
        }

        public static void Establish(object unit) => InvokeMethods(_establish, unit);
        public static void Destroy(object unit) => InvokeMethods(_destroy, unit);
        public static void Because(object unit) => InvokeMethods(_because, unit);

        static void InvokeMethods(IEnumerable<MethodInfo> methods, object unit)
        {
            foreach (var method in methods) method.Invoke(unit, Array.Empty<object>());
        }
    }
}
