using System.Reactive.Subjects;
using Aksio.Events.EventLogs;
using Cratis.Changes;
using Cratis.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents import operations that can be performed.
    /// </summary>
    /// <typeparam name="TModel">Type of model the operations are for.</typeparam>
    /// <typeparam name="TExternalModel">Type of external model the operations are for.</typeparam>
    public class ImportOperations<TModel, TExternalModel>
    {
        readonly AdapterFor<TModel, TExternalModel> _adapter;
        readonly TModel _initialState;
        readonly ISubject<ImportContext<TModel, TExternalModel>> _importContexts;
        readonly IEventLog _eventLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportOperations{TModel, TExternalModel}"/> class.
        /// </summary>
        /// <param name="adapter">The <see cref="AdapterFor{TModel, TExternalModel}"/>.</param>
        /// <param name="initialState">The initial state of the model.</param>
        /// <param name="importContexts"><see cref="IObservable{T}"/> of <see cref="ImportContext{TModel, TExternalModel}"/>.</param>
        /// <param name="eventLog">The <see cref="IEventLog"/> to work with.</param>
        public ImportOperations(
            AdapterFor<TModel, TExternalModel> adapter,
            TModel initialState,
            ISubject<ImportContext<TModel, TExternalModel>> importContexts,
            IEventLog eventLog)
        {
            _adapter = adapter;
            _initialState = initialState;
            _importContexts = importContexts;
            _eventLog = eventLog;
        }

        /// <summary>
        /// Apply an instance of the external model.
        /// </summary>
        /// <param name="instance">The external model instance.</param>
        /// <remarks>
        /// Objects will be mapped to the model and compared for changes and then run through
        /// the translation of changes to events.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Apply(TExternalModel instance)
        {
            var changeset = new Changeset<TExternalModel, TModel>(instance, _initialState);
            var context = new ImportContext<TModel, TExternalModel>(changeset, new EventsToAppend());
            _importContexts.OnNext(context);

            var eventSourceId = _adapter.Key.GetPropertyInfo().GetValue(instance) as EventSourceId;
            foreach (var @event in context.Events)
            {
                await _eventLog.Append(eventSourceId!, @event);
            }
        }

        /// <summary>
        /// Apply instances of the external model.
        /// </summary>
        /// <param name="instances">The external model instances.</param>
        /// <remarks>
        /// Objects will be mapped to the model and compared for changes and then run through
        /// the translation of changes to events.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Apply(IEnumerable<TExternalModel> instances)
        {
            foreach (var instance in instances)
            {
                await Apply(instance);
            }
        }
    }
}
