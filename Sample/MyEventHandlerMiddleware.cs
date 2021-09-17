using Aksio.Events.Handling;
using Dolittle.SDK.Events;

namespace Sample
{
    public class MyEventHandlerMiddleware : IEventHandlerMiddleware
    {
        public Task Invoke(EventContext eventContext, object @event, NextEventHandlerMiddleware next)
        {
            return next();
        }
    }
}