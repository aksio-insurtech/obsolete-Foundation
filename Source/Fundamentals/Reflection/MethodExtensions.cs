using System.Reflection;

#pragma warning disable RCS1047

namespace Aksio.Reflection
{
    /// <summary>
    /// Provides a set of methods for working with methods, such as <see cref="MethodInfo"/>.
    /// </summary>
    public static class MethodExtensions
    {
        /// <summary>
        /// Check whether or not a <see cref="MethodInfo"/> is async or not.
        /// </summary>
        /// <param name="methodInfo"><see cref="MethodInfo"/> to check.</param>
        /// <returns>True if is async, false if not.</returns>
        public static bool IsAsync(this MethodInfo methodInfo)
        {
            return methodInfo.ReturnType.IsAssignableTo(typeof(Task));
        }
    }
}
#pragma warning restore
