using System.Reflection;
using Aksio.Microservices.Configuration;
using Aksio.Reflection;
using Aksio.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for setting up configuration for host.
    /// </summary>
    public static class ConfigurationHostBuilderExtensions
    {
        static ConfigurationHostBuilderExtensions()
        {
            Configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                  .Build();
        }

        internal static IConfiguration Configuration { get; }

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

        /// <summary>
        /// Use configuration objects through discovery based on objects adorned with <see cref="ConfigurationAttribute"/>.
        /// </summary>
        /// <param name="builder"><see cref="IHostBuilder"/> to use with.</param>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        /// <param name="baseRelativePath">Optional base relative path, relative to the current running directory.</param>
        /// <returns><see cref="IHostBuilder"/> for continuation.</returns>
        public static IHostBuilder UseConfigurationObjects(this IHostBuilder builder, ITypes types, string baseRelativePath = "")
        {
            foreach (var configurationObject in types.All.Where(_ => _.HasAttribute<ConfigurationAttribute>()))
            {
                var attribute = configurationObject.GetCustomAttribute<ConfigurationAttribute>()!;

                var fileName = Path.HasExtension(attribute.FileName) ? attribute.FileName : $"{attribute.FileName}.json";
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), baseRelativePath))
                    .AddJsonFile(fileName, attribute.Optional)
                    .Build();

                var configurationInstance = configuration.Get(configurationObject);
                builder.ConfigureServices(_ =>
                {
                    _.AddSingleton(configurationInstance);

                    var optionsType = typeof(IOptions<>).MakeGenericType(configurationObject);
                    _.AddSingleton(optionsType);
                });
            }

            return builder;
        }
    }
}