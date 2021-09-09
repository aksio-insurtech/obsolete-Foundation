using Aksio.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseAksio(this IHostBuilder builder)
        {
            var types = new Types();

            builder.ConfigureServices(_ => _.AddSingleton<ITypes>(types));

            builder
                .UseDefaultConfiguration()
                .UseDefaultLogging()
                .UseDefaultDependencyInversion(types)
                .UseExecutionContext();

            return builder;
        }
    }
}