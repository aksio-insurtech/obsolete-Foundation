using Dolittle.SDK.Tenancy;

namespace Aksio.Dolittle.Resources
{
    /// <summary>
    /// Exception that gets thrown when a resource configuration for a specific tenant is missing.
    /// </summary>
    public class MissingResourceConfigurationsForTenant : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingResourceConfigurationsForTenant"/> class.
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> that is missing for.</param>
        public MissingResourceConfigurationsForTenant(TenantId tenantId)
            : base($"Missing resource configurations for '{tenantId}'")
        {
        }
    }
}
