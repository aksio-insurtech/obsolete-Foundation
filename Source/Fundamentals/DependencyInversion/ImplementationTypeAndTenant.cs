using Dolittle.SDK.Tenancy;

namespace Aksio.DependencyInversion
{
    /// <summary>
    /// Represents a link between a <see cref="Type"/> and a <see cref="TenantId"/>.
    /// </summary>
    /// <param name="ImplementationType"><see cref="Type"/>.</param>
    /// <param name="TenantId"><see cref="TenantId"/>.</param>
    public record ImplementationTypeAndTenant(Type ImplementationType, TenantId TenantId);
}
