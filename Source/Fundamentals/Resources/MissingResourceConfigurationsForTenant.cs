using Dolittle.SDK.Tenancy;

namespace Aksio.Resources
{
    /// <summary>
    /// Exception that gets thrown when missing a resource configuration for a <see cref="TenantId"/>.
    /// </summary>
    public class MissingResourceConfigurationsForTenant : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingResourceConfigurationsForTenant"/> class.
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> that is missing.</param>
        public MissingResourceConfigurationsForTenant(TenantId tenantId)
            : base($"Missing resource configurations for '{tenantId}'")
        {
        }
    }
}
