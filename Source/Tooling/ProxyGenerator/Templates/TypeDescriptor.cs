namespace Aksio.ProxyGenerator.Templates
{
    /// <summary>
    /// Describes a type for templating purposes.
    /// </summary>
    /// <param name="Name">Name of the type.</param>
    /// <param name="Properties">Properties on the type.</param>
    /// <param name="Imports">Additional import statements.</param>
    public record TypeDescriptor(string Name, IEnumerable<PropertyDescriptor> Properties, IEnumerable<ImportStatement> Imports);
}