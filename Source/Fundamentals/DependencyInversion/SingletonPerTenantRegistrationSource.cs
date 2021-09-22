using System.Collections.Concurrent;
using Aksio.Execution;
using Aksio.Reflection;
using Aksio.Types;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Aksio.DependencyInversion
{
    /// <summary>
    /// Represents a <see cref="IRegistrationSource"/> to handle correct lifecycle for implementations marked with <see cref="SingletonPerTenantAttribute"/>.
    /// </summary>
    public class SingletonPerTenantRegistrationSource : IRegistrationSource
    {
        readonly ConcurrentDictionary<ImplementationTypeAndTenant, object> _instancesPerTenant = new();

        readonly ContractToImplementorsMap _implementorsMap = new();

        IContainer? _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonPerTenantRegistrationSource"/> class.
        /// </summary>
        /// <param name="containerBuilder">The <see cref="ContainerBuilder"/> this registration source is registered in.</param>
        /// <param name="types"><see cref="ITypes"/> to discover implementors marked with <see cref="SingletonPerTenantAttribute"/>.</param>
        public SingletonPerTenantRegistrationSource(ContainerBuilder containerBuilder, ITypes types)
        {
            containerBuilder.RegisterBuildCallback(_ => _container = (_ as IContainer)!);
            _implementorsMap.Feed(types.All.Where(_ => _.HasAttribute<SingletonPerTenantAttribute>()));
        }

        /// <inheritdoc/>
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc/>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            if (!(service is IServiceWithType) || service is not IServiceWithType serviceWithType) return Enumerable.Empty<IComponentRegistration>();

            var implementors = _implementorsMap.GetImplementorsFor(serviceWithType.ServiceType);
            if (!implementors.Any()) return Enumerable.Empty<IComponentRegistration>();

            var implementationType = implementors.First();
            var registration = RegistrationBuilder
                                .ForDelegate(implementors.First(), (_, __) => Resolve(implementationType))
                                .As(serviceWithType.ServiceType).CreateRegistration();

            return new[] { registration };
        }

        object Resolve(Type implementationType)
        {
            var executionContextManager = _container!.Resolve<IExecutionContextManager>();
            var key = new ImplementationTypeAndTenant(implementationType, executionContextManager.Current.Tenant);
            if (_instancesPerTenant.ContainsKey(key))
            {
                return _instancesPerTenant[key];
            }

            return _instancesPerTenant[key] = ContainerBuilderExtensions.Container!.Resolve(implementationType);
        }
    }
}
