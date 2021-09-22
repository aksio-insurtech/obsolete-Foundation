using Dolittle.SDK.Tenancy;

namespace Aksio.Resources
{
    /// <summary>
    /// Represents the configurations for all resources.
    /// </summary>
    public interface IResourceConfigurations
    {
        /// <summary>
        /// Get a resource configuration for a specific type.
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> to get for.</param>
        /// <typeparam name="TResource">Type of resource.</typeparam>
        /// <returns>The <see cref="ResourceConfiguration"/>.</returns>
        TResource GetFor<TResource>(TenantId tenantId)
            where TResource : ResourceConfiguration;
    }
}
