using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cratis.Types;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for working with dependency inversion.
    /// </summary>
    public static class DependencyInversionHostBuilderExtensions
    {
        /// <summary>
        /// Use the default dependency inversion setup.
        /// </summary>
        /// <param name="builder"><see cref="IHostBuilder"/> to use with.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <returns><see cref="IHostBuilder"/> for continuation.</returns>
        public static IHostBuilder UseDefaultDependencyInversion(this IHostBuilder builder, ITypes types)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterDefaults(types));
            return builder;
        }
    }
}