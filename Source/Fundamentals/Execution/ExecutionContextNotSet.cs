using ExecutionContext = Dolittle.SDK.Execution.ExecutionContext;

namespace Aksio.Execution
{
    /// <summary>
    /// Exception that gets thrown when the ExecutionContext is not set.
    /// </summary>
    public class ExecutionContextNotSet : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextNotSet"/> class.
        /// </summary>
        public ExecutionContextNotSet()
            : base("The execution context has not been established")
        {
        }

        /// <summary>
        /// Throw <see cref="ExecutionContextNotSet"/> if it is not set.
        /// </summary>
        /// <param name="context"><see cref="ExecutionContext"/> ref to assert.</param>
        /// <exception cref="ExecutionContextNotSet">Thrown if the ref is null.</exception>
        public static void ThrowIfNotSet(ExecutionContext? context)
        {
            if (context != null) throw new ExecutionContextNotSet();
        }
    }
}