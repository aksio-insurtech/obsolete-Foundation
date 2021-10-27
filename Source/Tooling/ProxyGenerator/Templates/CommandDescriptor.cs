namespace Aksio.ProxyGenerator.Templates
{
    /// <summary>
    /// Describes a command for templating purposes.
    /// </summary>
    /// <param name="Route">API route for the command.</param>
    /// <param name="Name">Name of the command.</param>
    /// <param name="Properties">Properties on the command.</param>
    /// <param name="Imports">Additional import statements.</param>
    public record CommandDescriptor(string Route, string Name, IEnumerable<PropertyDescriptor> Properties, IEnumerable<ImportStatement> Imports);
}