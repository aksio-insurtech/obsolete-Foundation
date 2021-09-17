using Aksio.Types;
using Dolittle.SDK.Events.Handling.Builder;

namespace Aksio.Events.Handling
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHandlerMiddlewares"/>.
    /// </summary>
    public class EventHandlerMiddlewares : IEventHandlerMiddlewares
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerMiddlewares"/> class.
        /// </summary>
        /// <param name="types"><see cref="ITypes"/> for type discovery.</param>
        public EventHandlerMiddlewares(ITypes types)
        {
            All = types.FindMultiple<IEventHandlerMiddleware>().Select(_ => (IEventHandlerMiddleware)EventHandlers.ServiceProvider!.GetService(_)!);
        }

        /// <inheritdoc/>
        public IEnumerable<IEventHandlerMiddleware> All { get; }

        /// <inheritdoc/>
        public TaskEventHandlerSignature CreateInvokerFor(EventHandlerMethod method)
        {
            return async (@event, context) =>
            {
                NextEventHandlerMiddleware next = async () => await method.Invoke(@event, context).ConfigureAwait(false);

                NextEventHandlerMiddleware WrapMiddleware(IEventHandlerMiddleware middleware, NextEventHandlerMiddleware nextMiddleware) => () => middleware.Invoke(context, @event, nextMiddleware);

                foreach (var middleware in All.Reverse())
                {
                    next = WrapMiddleware(middleware, next);
                }

                await next().ConfigureAwait(false);
            };
        }
    }
}
