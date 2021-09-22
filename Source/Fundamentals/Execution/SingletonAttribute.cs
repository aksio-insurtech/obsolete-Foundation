namespace Aksio.Execution
{
    /// <summary>
    /// Attribute to adorn types for the IoC hookup to recognize it as a Singleton.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
    }
}
