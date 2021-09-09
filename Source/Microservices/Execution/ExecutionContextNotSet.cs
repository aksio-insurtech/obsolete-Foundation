using ExecutionContext = Dolittle.SDK.Execution.ExecutionContext;

namespace Aksio.Microservices.Execution
{
    /// <summary>
    /// Exception that gets thrown when the ExecutionContext is not set.
    /// </summary>
    public class ExecutionContextNotSet : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextNotSet"/>.
        /// </summary>
        public ExecutionContextNotSet() : base("The execution context has not been established") { }

        public static void ThrowIfNotSet(ExecutionContext? context)
        {
            if (context != null) throw new ExecutionContextNotSet();
        }
    }
}