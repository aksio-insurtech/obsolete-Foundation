using Aksio.Microservices.Dolittle;
using Dolittle.SDK;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides extension methods for configuring Dolittle with <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class DolittleApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Dolittle with a given <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="applicationBuilder"><see cref="IApplicationBuilder"/> to use it for.</param>
        /// <returns><see cref="IApplicationBuilder"/> for continuation.</returns>
        public static IApplicationBuilder UseDolittle(this IApplicationBuilder applicationBuilder)
        {
            var client = applicationBuilder.ApplicationServices.GetService<Client>();
            var executionContextManager = applicationBuilder.ApplicationServices.GetService<IExecutionContextManager>();

            client!.WithContainer(new DolittleContainer(applicationBuilder.ApplicationServices, executionContextManager!)).Start();

            return applicationBuilder;
        }
    }
}