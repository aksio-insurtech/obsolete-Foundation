using Aksio.Application;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public  static IApplicationBuilder UseAksio(this IApplicationBuilder app)
        {
            app.UseDefaultLogging();

            if (RuntimeEnvironment.IsDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}
