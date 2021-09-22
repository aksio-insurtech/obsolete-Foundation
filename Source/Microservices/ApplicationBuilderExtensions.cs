using Aksio.Execution;
using Aksio.Microservices;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides extension methods for the application builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Aksio default setup.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> to extend.</param>
        /// <returns><see cref="IApplicationBuilder"/> for continuation.</returns>
        public static IApplicationBuilder UseAksio(this IApplicationBuilder app)
        {
            Internals.ServiceProvider = app.ApplicationServices;

            app.UseExecutionContext();
            app.UseDefaultLogging();
            app.UseDolittle();

            if (RuntimeEnvironment.IsDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}
