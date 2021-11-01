namespace Aksio.ProxyGenerator.Templates
{
    /// <summary>
    /// Describes a query argument for templating purposes.
    /// </summary>
    /// <param name="Name">Name of argument.</param>
    /// <param name="Type">Type of argument.</param>
    /// <param name="IsOptional">Whether or not the argument is nullable / optional.</param>
    public record QueryArgumentDescriptor(string Name, string Type, bool IsOptional);
}