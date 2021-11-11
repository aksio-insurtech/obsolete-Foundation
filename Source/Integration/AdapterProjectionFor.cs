using Cratis.Events.Projections;
using Cratis.Events.Projections.Changes;
using Cratis.Extensions.Dolittle.EventStore;
using Microsoft.Extensions.Logging;
using EventSourceId = Dolittle.SDK.Events.EventSourceId;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IAdapterProjectionFor{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public class AdapterProjectionFor<TModel> : IAdapterProjectionFor<TModel>
    {
        readonly IProjection _projection;
        readonly IEventStream _eventStream;
        readonly ILoggerFactory _loggerFactory;
        readonly IChangesetStorage _changesetStorage = new NullChangesetStorage();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdapterProjectionFor{TModel}"/> class.
        /// </summary>
        /// <param name="projection"><see cref="IProjection"/> to use.</param>
        /// <param name="eventStream"><see cref="IEventStream"/> to work with.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for creating loggers.</param>
        public AdapterProjectionFor(
            IProjection projection,
            IEventStream eventStream,
            ILoggerFactory loggerFactory)
        {
            _projection = projection;
            _eventStream = eventStream;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public TModel GetById(EventSourceId eventSourceId)
        {
            var eventProvider = new EventSourceInstanceEventProvider(_eventStream, eventSourceId);
            var pipeline = new ProjectionPipeline(eventProvider, _projection, _changesetStorage, _loggerFactory.CreateLogger<ProjectionPipeline>());
            var result = new InstanceProjectionResult();
            pipeline.StoreIn(result);
            pipeline.Start();

            return default!;
        }
    }
}
