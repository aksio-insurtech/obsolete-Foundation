using Aksio.Microservices.Dolittle;
using Dolittle.SDK.Tenancy;

namespace Microsoft.AspNetCore.Builder
{
    public static class ExecutionContextAppBuilderExtensions
    {
        const string TenantIdHeaderKey = "Tenant-ID";

        public static void UseExecutionContext(this IApplicationBuilder app)
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
        }
    }
}
