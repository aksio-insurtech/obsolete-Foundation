using Aksio.Dolittle.Resources;
using Aksio.Execution;
using Autofac;
using Dolittle.SDK.Tenancy;
using MongoDB.Driver;

namespace Aksio.MongoDB
{
    /// <summary>
    /// Holds the <see cref="Autofac.Module"/> for hooking up bindings for MongoDB.
    /// </summary>
    public class Module : Autofac.Module
    {
        static readonly Dictionary<TenantId, IMongoDatabase> _mongoDatabaseByTenant = new();

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            IContainer? container = null;
            builder.RegisterBuildCallback(_ => container = _ as IContainer);
            builder.Register(_ =>
            {
                var tenant = container!.Resolve<IExecutionContextManager>().Current.Tenant;
                if (_mongoDatabaseByTenant.ContainsKey(tenant))
                {
                    return _mongoDatabaseByTenant[tenant];
                }

                var resourceConfigurations = container!.Resolve<IResourceConfigurations>();
                var config = resourceConfigurations.GetFor<MongoDbReadModelsConfiguration>(tenant);
                var url = MongoUrl.Create(config.Host);
                var settings = MongoClientSettings.FromUrl(url);
                var client = new MongoClient(settings.Freeze());
                var database = client.GetDatabase(config.Database);
                _mongoDatabaseByTenant[tenant] = database;
                return database;
            }).As<IMongoDatabase>();
        }
    }
}