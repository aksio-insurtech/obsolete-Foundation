using Dolittle.SDK.Tenancy;

namespace Aksio.Dolittle.Resources
{
    /// <summary>
    /// Defines a system for working with Dolittle resources.
    /// </summary>
    public interface IResourceConfigurations
    {
        /// <summary>
        /// Get a specific resource based on type.
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> to get for.</param>
        /// <typeparam name="TResource">Type of resource to get.</typeparam>
        /// <returns>Resource instance, if any.</returns>
        /// <exception cref="MissingResourceConfigurationsForTenant">If no resource configuration exist for tenant.</exception>
        /// <exception cref="MissingResourceConfigurationOfType">If resource type is unknown.</exception>
        TResource GetFor<TResource>(TenantId tenantId)
            where TResource : ResourceConfiguration;
    }
}
