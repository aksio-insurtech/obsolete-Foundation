using Serilog;

namespace Microsoft.Extensions.Hosting
{
    public static class LoggingHostBuilderExtensions
    {
        public static IHostBuilder UseDefaultLogging(this IHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigurationHostBuilderExtensions.Configuration)
                .CreateLogger();

            builder.UseSerilog();

            return builder;
        }
    }
}