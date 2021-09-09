using Aksio.Microservices.Dolittle;
using Aksio.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class ExecutionHostBuilderExtensions
    {
        public static IHostBuilder UseExecutionContext(this IHostBuilder builder)
        {
            var types = new Types();

            builder.ConfigureServices(_ => _.AddSingleton<IExecutionContextManager, ExecutionContextManager>());

            return builder;
        }
    }
}