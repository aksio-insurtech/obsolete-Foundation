using Aksio.Execution;
using Autofac;
using Dolittle.SDK.Tenancy;
using MongoDB.Driver;

namespace Aksio.Resources
{
    /// <summary>
    /// Represents a <see cref="Module"/> for setting up defaults and bindings for resources.
    /// </summary>
    public class ResourcesModule : Module
    {
        readonly Dictionary<TenantId, IMongoDatabase> _mongoDatabaseByTenant = new();

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            IContainer? container = default;
            builder.RegisterBuildCallback(_ => container = (_ as IContainer)!);

            builder.Register(_ =>
            {
                if (container != default)
                {
                    var executionContextManager = container.Resolve<IExecutionContextManager>();
                    var tenant = executionContextManager.Current.Tenant;
                    var resourceConfigurations = container.Resolve<IResourceConfigurations>();
                    return resourceConfigurations.GetFor<EventStoreConfiguration>(tenant);
                }

                return null!;
            }).As<EventStoreConfiguration>();

            builder.Register(_ =>
            {
                if (container != default)
                {
                    var executionContextManager = container.Resolve<IExecutionContextManager>();
                    var tenant = executionContextManager.Current.Tenant;
                    var resourceConfigurations = container.Resolve<IResourceConfigurations>();
                    return resourceConfigurations.GetFor<MongoDbReadModelsConfiguration>(tenant);
                }

                return null!;
            }).As<MongoDbReadModelsConfiguration>();

            builder.Register(_ =>
            {
                if (container != default)
                {
                    var executionContextManager = container.Resolve<IExecutionContextManager>();
                    var tenant = executionContextManager.Current.Tenant;
                    if (_mongoDatabaseByTenant.ContainsKey(tenant))
                    {
                        return _mongoDatabaseByTenant[tenant];
                    }

                    var resourceConfigurations = container.Resolve<IResourceConfigurations>();
                    var config = resourceConfigurations.GetFor<MongoDbReadModelsConfiguration>(tenant);
                    var url = MongoUrl.Create(config.Host);
                    var settings = MongoClientSettings.FromUrl(url);
                    var client = new MongoClient(settings.Freeze());
                    var database = client.GetDatabase(config.Database);
                    _mongoDatabaseByTenant[tenant] = database;
                    return database;
                }

                return null!;
            }).As<IMongoDatabase>();
        }
    }
}
