using System.Dynamic;
using Aksio.Execution;
using Cratis.Collections;
using Cratis.Types;
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
        readonly IInstancesOf<ICanProvideAdditionalEventInformation> _additionalEventInformationProviders;
        readonly Client _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        /// <param name="client">The Dolittle <see cref="Client"/>.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for working with tenancy.</param>
        /// <param name="eventTypes"><see cref="IEventTypes"/> register.</param>
        /// <param name="additionalEventInformationProviders"><see cref="IInstancesOf{T}"/> <see cref="ICanProvideAdditionalEventInformation"/>.</param>
        public EventLog(
            Client client,
            IExecutionContextManager executionContextManager,
            IEventTypes eventTypes,
            IInstancesOf<ICanProvideAdditionalEventInformation> additionalEventInformationProviders)
        {
            _executionContextManager = executionContextManager;
            _eventTypes = eventTypes;
            _additionalEventInformationProviders = additionalEventInformationProviders;
            _client = client;
        }

        /// <inheritdoc/>
        public async Task Append(EventSourceId eventSourceId, object @event)
        {
            var eventType = _eventTypes.GetFor(@event.GetType());
            var expando = new ExpandoObject();
            AddPropertiesFrom(expando, @event);
            _additionalEventInformationProviders.ForEach(_ => AddPropertiesFrom(expando, _.ProvideFor(@event)));

            await _client.EventStore.ForTenant(_executionContextManager.Current.Tenant)
                .Commit(_ => _
                    .CreateEvent(expando)
                    .FromEventSource(eventSourceId)
                    .WithEventType(eventType));
        }

        void AddPropertiesFrom(ExpandoObject expando, object input)
        {
            foreach (var property in input.GetType().GetProperties())
            {
                expando.TryAdd(property.Name, property.GetValue(input));
            }
        }
    }
}