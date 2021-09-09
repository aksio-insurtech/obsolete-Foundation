namespace Microsoft.AspNetCore.Builder
{
    public static class LoggingApplicationBuilderExtensions
    {
        public  static IApplicationBuilder UseDefaultLogging(this IApplicationBuilder app)
        {
            app.UseHttpLogging();

            return app;
        }
    }
}
