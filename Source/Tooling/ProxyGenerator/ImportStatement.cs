namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// Describes an import statement.
    /// </summary>
    /// <param name="Type">Type to use.</param>
    /// <param name="Module">Source module in which the type is from.</param>
    public record ImportStatement(string Type, string Module);
}