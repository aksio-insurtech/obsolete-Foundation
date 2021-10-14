using Aksio;
using Aksio.Execution;

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

            if (RuntimeEnvironment.IsDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger"));
            }

            app.UseDefaultLogging()
                .UseDolittle()
                .UseDolittleExecutionContext()
                .UseDolittleSchemaStore()
                .UseDolittleProjections()
                .UseCratisWorkbench();

            return app;
        }
    }
}
