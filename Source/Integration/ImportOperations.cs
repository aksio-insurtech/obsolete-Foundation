using System.Reactive.Subjects;
using Aksio.Events.EventLogs;
using Cratis.Changes;
using Cratis.Reflection;
using Dolittle.SDK.Events;

namespace Aksio.Integration
{
    /// <summary>
    /// Represents an implementation of <see cref="IImportOperations{TModel, TExternalModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model the operations are for.</typeparam>
    /// <typeparam name="TExternalModel">Type of external model the operations are for.</typeparam>
    public class ImportOperations<TModel, TExternalModel> : IImportOperations<TModel, TExternalModel>
    {
        readonly IAdapterFor<TModel, TExternalModel> _adapter;
        readonly IAdapterProjectionFor<TModel> _adapterProjection;
        readonly Subject<ImportContext<TModel, TExternalModel>> _importContexts;
        readonly IEventLog _eventLog;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportOperations{TModel, TExternalModel}"/> class.
        /// </summary>
        /// <param name="adapter">The <see cref="IAdapterFor{TModel, TExternalModel}"/>.</param>
        /// <param name="adapterProjection">The <see cref="IAdapterProjectionFor{TModel}"/> for the model.</param>
        /// <param name="eventLog">The <see cref="IEventLog"/> to work with.</param>
        public ImportOperations(
            IAdapterFor<TModel, TExternalModel> adapter,
            IAdapterProjectionFor<TModel> adapterProjection,
            IEventLog eventLog)
        {
            _adapter = adapter;
            _adapterProjection = adapterProjection;
            _importContexts = new();
            _adapter.DefineImport(new ImportBuilderFor<TModel, TExternalModel>(_importContexts));
            _eventLog = eventLog;
        }

        /// <inheritdoc/>
        public async Task Apply(TExternalModel instance)
        {
            var eventSourceId = _adapter.Key.GetPropertyInfo().GetValue(instance) as EventSourceId;
            var initial = _adapterProjection.GetById(eventSourceId!);
            var changeset = new Changeset<TExternalModel, TModel>(instance, initial);

            // Map external instance and then perform comparison
            var context = new ImportContext<TModel, TExternalModel>(changeset, new EventsToAppend());
            _importContexts.OnNext(context);

            foreach (var @event in context.Events)
            {
                await _eventLog.Append(eventSourceId!, @event);
            }
        }

        /// <inheritdoc/>
        public async Task Apply(IEnumerable<TExternalModel> instances)
        {
            foreach (var instance in instances)
            {
                await Apply(instance);
            }
        }

        /// <inheritdoc/>
        public void Dispose() => _importContexts.Dispose();
    }
}
