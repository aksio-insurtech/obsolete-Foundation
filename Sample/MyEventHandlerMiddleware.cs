using Aksio.Events.Handling;
using Dolittle.SDK.Events;

namespace Sample
{
    public class MyEventHandlerMiddleware : IEventHandlerMiddleware
    {
        readonly ILogger<MyEventHandlerMiddleware> _logger;

        public MyEventHandlerMiddleware(ILogger<MyEventHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(EventContext eventContext, object @event, NextEventHandlerMiddleware next)
        {
            var before = DateTime.UtcNow;
            await next().ConfigureAwait(false);
            var after = DateTime.UtcNow;
            var delta = after.Subtract(before);
            _logger.HandlerTiming(delta.TotalMilliseconds, @event.GetType().Name);
        }
    }
}