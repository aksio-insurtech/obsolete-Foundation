#pragma warning disable RCS1213, CA1823, IDE0052, SA1201

namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// Represents a target type and optional import module.
    /// </summary>
    /// <param name="Type">Type.</param>
    /// <param name="ImportFromModule">Module to import from. default or empty means no need to import.</param>
    public record TargetType(string Type, string ImportFromModule = "");
}