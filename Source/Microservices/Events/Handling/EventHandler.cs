using Dolittle.SDK.Events;
using Dolittle.SDK.Events.Handling;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an event handler.
    /// </summary>
    /// <param name="Id">The unique identifier for the <see cref="EventHandler"/>.</param>
    /// <param name="Partitioned">Whether or not the <see cref="EventHandler"/> is scoped.</param>
    /// <param name="Scope"><see cref="ScopeId"/> the <see cref="EventHandler"/> works on.</param>
    /// <param name="Target">The target <see cref="Type"/>.</param>
    /// <param name="Methods">All <see cref="EventHandlerMethod"/> registered to the <see cref="IEventHandler"/>.</param>
    public record EventHandler(EventHandlerId Id, bool Partitioned, ScopeId Scope, Type Target, IEnumerable<EventHandlerMethod> Methods);
}