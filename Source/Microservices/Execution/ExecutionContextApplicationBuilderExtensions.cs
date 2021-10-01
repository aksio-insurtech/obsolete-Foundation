using Aksio.Execution;
using Dolittle.SDK.Tenancy;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for setting up execution context.
    /// </summary>
    public static class ExecutionContextApplicationBuilderExtensions
    {
        const string TenantIdHeaderKey = "Tenant-ID";

        /// <summary>
        /// Add a middleware for handling the Dolittle execution context automatically.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> to extend.</param>
        /// <returns><see cref="IApplicationBuilder"/> for continuation.</returns>
        public static IApplicationBuilder UseDolittleExecutionContext(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var tenantId = TenantId.Development;

                if (context.Request.Headers.ContainsKey(TenantIdHeaderKey))
                {
                    tenantId = context.Request.Headers[TenantIdHeaderKey].First();
                }

                var executionContextManager = app.ApplicationServices.GetService<IExecutionContextManager>();
                executionContextManager!.Establish(tenantId, Guid.NewGuid());
                await next.Invoke().ConfigureAwait(false);
            });

            return app;
        }
    }
}
