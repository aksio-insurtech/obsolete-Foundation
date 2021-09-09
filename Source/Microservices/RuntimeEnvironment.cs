namespace Aksio.Microservices
{
    /// <summary>
    /// Represents helpers for getting details about the Runtime environment.
    /// </summary>
    public static class RuntimeEnvironment
    {
        static RuntimeEnvironment()
        {
            IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        /// <summary>
        /// Gets whether or not we're running in development or not.
        /// </summary>
        public static readonly bool IsDevelopment;
    }
}
