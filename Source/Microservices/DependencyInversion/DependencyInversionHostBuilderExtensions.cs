using Aksio.Types;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class DependencyInversionHostBuilderExtensions
    {
        public static IHostBuilder UseDefaultDependencyInversion(this IHostBuilder builder, ITypes types)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterDefaults(types));
            return builder;
        }
    }
}