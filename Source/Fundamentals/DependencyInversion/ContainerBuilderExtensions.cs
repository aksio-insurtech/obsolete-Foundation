using Autofac;
using Aksio.Types;

namespace Aksio.DependencyInversion
{
    /// <summary>
    /// Represents extension methods for the Autofac <see cref="ContainerBuilder"/>.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        internal static ITypes Types;
        internal static IContainer Container;
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Register default Aksio conventions and registrations into the Autofac container.
        /// </summary>
        /// <param name="containerBuilder"><see cref="ContainerBuilder"/> to register into.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <returns><see cref="ContainerBuilder"/> for build continuation.</returns>
        public static ContainerBuilder RegisterDefaults(this ContainerBuilder containerBuilder, ITypes types)
        {
            Types = types;
            containerBuilder.RegisterInstance(types).As<ITypes>();
            foreach (var moduleType in types.FindMultiple<Module>())
            {
                containerBuilder.RegisterModule(Activator.CreateInstance(moduleType) as Module);
            }

            containerBuilder.RegisterSource<SelfBindingRegistrationSource>();
            containerBuilder.Register(_ => Container).As<IContainer>().SingleInstance();
            containerBuilder.RegisterBuildCallback(_ => Container = _ as IContainer);

            containerBuilder.RegisterSource(new ProviderForRegistrationSource());

            return containerBuilder;
        }
    }
}
