using Aksio.Microservices.Dolittle;
using Aksio.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Provides extension methods for <see cref="IHostBuilder"/>.
    /// </summary>
    public static class ExecutionContextHostBuilderExtensions
    {
        /// <summary>
        /// Use execution context with the host.
        /// </summary>
        /// <param name="builder"><see cref="IHostBuilder"/> to extend.</param>
        /// <returns><see cref="IHostBuilder"/> for building continuation.</returns>
        public static IHostBuilder UseExecutionContext(this IHostBuilder builder)
        {
            var types = new Types();

            builder.ConfigureServices(_ => _.AddSingleton<IExecutionContextManager, ExecutionContextManager>());

            return builder;
        }
    }
}