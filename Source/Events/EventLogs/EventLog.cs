using Aksio.Execution;
using Dolittle.SDK;
using Dolittle.SDK.Events;
using IEventTypes = Aksio.Events.Types.IEventTypes;

namespace Aksio.Events.EventLogs
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventLog"/>.
    /// </summary>
    public class EventLog : IEventLog
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IEventTypes _eventTypes;
        readonly Client _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        /// <param name="client">The Dolittle <see cref="Client"/>.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for working with tenancy.</param>
        /// <param name="eventTypes"><see cref="IEventTypes"/> register.</param>
        public EventLog(Client client, IExecutionContextManager executionContextManager, IEventTypes eventTypes)
        {
            _executionContextManager = executionContextManager;
            _eventTypes = eventTypes;
            _client = client;
        }

        /// <inheritdoc/>
        public async Task Append(EventSourceId eventSourceId, object @event)
        {
            var eventType = _eventTypes.GetFor(@event.GetType());
            await _client.EventStore.ForTenant(_executionContextManager.Current.Tenant)
                .Commit(_ => _
                    .CreateEvent(@event)
                    .FromEventSource(eventSourceId)
                    .WithEventType(eventType))
                .ConfigureAwait(false);
        }
    }
}