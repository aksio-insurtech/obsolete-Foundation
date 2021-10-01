using Dolittle.SDK.Events.Handling.Builder;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Defines a system that manages all the <see cref="IEventHandlerMiddleware"/>.
    /// </summary>
    public interface IEventHandlerMiddlewares
    {
        /// <summary>
        /// Gets all <see cref="IEventHandlerMiddleware"/>.
        /// </summary>
        IEnumerable<IEventHandlerMiddleware> All { get; }

        /// <summary>
        /// Create a method invoker for a specific <see cref="EventHandlerMethod"/>.
        /// </summary>
        /// <param name="method"><see cref="EventHandlerMethod"/> to create for.</param>
        /// <returns><see cref="TaskEventHandlerSignature"/> ready to be used.</returns>
        TaskEventHandlerSignature CreateInvokerFor(EventHandlerMethod method);
    }
}