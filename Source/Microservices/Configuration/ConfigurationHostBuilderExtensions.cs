using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for setting up configuration for host.
    /// </summary>
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

        /// <summary>
        /// Use default configuration.
        /// </summary>
        /// <param name="builder"><see cref="IHostBuilder"/> to use with.</param>
        /// <returns><see cref="IHostBuilder"/> for continuation.</returns>
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