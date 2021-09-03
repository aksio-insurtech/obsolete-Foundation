
using System.Reflection;

namespace Aksio.BDD
{
    public class Specification : IDisposable
    {
        public Specification()
        {
            OnEstablish();
            OnBecause();
        }

        public void Dispose()
        {
            OnDestroy();
            GC.SuppressFinalize(this);
        }

        void OnEstablish()
        {
            InvokeMethod("Establish");
        }

        void OnBecause()
        {
            InvokeMethod("Because");
        }

        void OnDestroy()
        {
            InvokeMethod("Destroy");
        }

        void InvokeMethod(string name)
        {
            typeof(SpecificationMethods<>).MakeGenericType(GetType()).GetMethod(name, BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[] { this });
        }
    }
}
