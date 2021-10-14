using Aksio.Events.EventLogs;

namespace Sample
{
    public class MyAdditionalEventInformationProvider : ICanProvideAdditionalEventInformation
    {
        public object ProvideFor(object @event)
        {
            return new
            {
                Something = Guid.NewGuid(),
                SomeTime = DateTimeOffset.UtcNow
            };
        }
    }
}