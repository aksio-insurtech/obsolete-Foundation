using Dolittle.SDK.Events;
using Newtonsoft.Json.Schema;

namespace Events.Schemas
{
    /// <summary>
    /// Represents the schema of an event.
    /// </summary>
    /// <param name="EventType">The <see cref="EventType"/> represented.</param>
    /// <param name="Schema">The underlying <see cref="JSchema"/>.</param>
    public record EventSchema(EventType EventType, JSchema Schema);
}