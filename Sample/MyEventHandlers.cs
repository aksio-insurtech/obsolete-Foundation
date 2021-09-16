using Dolittle.SDK;
using Dolittle.SDK.Events.Handling;

namespace Sample
{
    [EventHandler("a5a55a35-1846-4386-b752-4d9a3da7aa10")]
    public class MyEventHandlers
    {
        readonly ILogger<MyEventHandlers> _logger;

        public MyEventHandlers(ILogger<MyEventHandlers> logger)
        {
            _logger = logger;
        }

        public Task DoTheThing(MyEvent @event)
        {
            _logger.LogInformation("We're here");

            return Task.CompletedTask;
        }

    }
}