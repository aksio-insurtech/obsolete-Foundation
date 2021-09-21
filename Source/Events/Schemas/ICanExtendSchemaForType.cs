using Newtonsoft.Json.Schema;

namespace Events.Schemas
{
    /// <summary>
    /// Defines something that can provide schema metadata for a specific type.
    /// </summary>
    public interface ICanExtendSchemaForType
    {
        /// <summary>
        /// Gets the type that schema information can be provided for.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Extend schema.
        /// </summary>
        /// <param name="schema"><see cref="JSchema"/> to provide for.</param>
        void Extend(JSchema schema);
    }
}