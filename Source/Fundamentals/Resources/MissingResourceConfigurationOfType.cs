using Dolittle.SDK.Tenancy;

namespace Aksio.Resources
{
    /// <summary>
    /// Exception that gets thrown when a resource configuration for a specific type is missing.
    /// </summary>
    public class MissingResourceConfigurationOfType : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingResourceConfigurationOfType"/> class.
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> the configuration is missing for.</param>
        /// <param name="type">Type of resource configuration.</param>
        public MissingResourceConfigurationOfType(TenantId tenantId, Type type)
            : base($"Missing resource configuration of type '{type.Name}' for tenant '${tenantId}'")
        {
        }
    }
}
