using System.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Provides a set of extension methods for working with event handler methods.
    /// </summary>
    public static class EventHandlerMethodsExtensions
    {
        /// <summary>
        /// Get handle methods for a type - if any.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get handle methods from.</param>
        /// <param name="eventTypesMap">Map of CLR type to <see cref="EventTypeId"/>.</param>
        /// <returns>Map of CLR type to <see cref="MethodInfo">handle method</see>.</returns>
        public static IDictionary<Type, MethodInfo> GetHandleMethods(this Type type, IDictionary<Type, EventType> eventTypesMap)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                            .Where(_ => IsHandleMethod(eventTypesMap, _))
                                            .ToDictionary(_ => _.GetParameters()[0].ParameterType, _ => _);
        }

        static bool IsHandleMethod(IDictionary<Type, EventType> eventTypes, MethodInfo methodInfo)
        {
            var isHandleMethod = methodInfo.ReturnType.IsAssignableTo(typeof(Task)) || methodInfo.ReturnType == typeof(void);
            if (!isHandleMethod) return false;
            var parameters = methodInfo.GetParameters();
            if (parameters.Length >= 1)
            {
                isHandleMethod = eventTypes.ContainsKey(parameters[0].ParameterType);
                if (parameters.Length == 2)
                {
                    isHandleMethod &= parameters[1].ParameterType == typeof(EventContext);
                }
                else if (parameters.Length > 2)
                {
                    isHandleMethod = false;
                }

                return isHandleMethod;
            }

            return false;
        }
    }
}