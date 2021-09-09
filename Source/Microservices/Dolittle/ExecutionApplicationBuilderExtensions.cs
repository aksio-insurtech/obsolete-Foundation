using Aksio.Microservices.Dolittle;
using Dolittle.SDK.Tenancy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for setting up execution context.
    /// </summary>
    public static class ExecutionContextAppBuilderExtensions
    {
        const string TenantIdHeaderKey = "Tenant-ID";

        /// <summary>
        /// Add a middleware for handling the Dolittle execution context automatically.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> to extend.</param>
        public static IApplicationBuilder UseExecutionContext(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var tenantId = TenantId.Development;

                if (context.Request.Headers.ContainsKey(TenantIdHeaderKey))
                {
                    tenantId = context.Request.Headers[TenantIdHeaderKey].First();
                }

                var executionContextManager = app.ApplicationServices.GetService(typeof(IExecutionContextManager)) as IExecutionContextManager;
                executionContextManager!.Establish(tenantId, Guid.NewGuid());
                await next.Invoke();
            });

            return app;
        }
    }
}
