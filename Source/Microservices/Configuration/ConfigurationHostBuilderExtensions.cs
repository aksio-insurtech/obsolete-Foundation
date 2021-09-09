using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting
{
    public static class ConfigurationHostBuilderExtensions
    {
        internal static IConfiguration Configuration { get; }

        static ConfigurationHostBuilderExtensions()
        {
            Configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                  .Build();
        }

        public static IHostBuilder UseDefaultConfiguration(this IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(_ =>
            {
                _.Sources.Clear();
                _.AddConfiguration(Configuration);
            });

            return builder;
        }
    }
}