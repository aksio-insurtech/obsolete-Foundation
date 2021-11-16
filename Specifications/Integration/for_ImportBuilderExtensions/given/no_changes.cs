using System.Reactive.Subjects;
using Cratis.Changes;
using ObjectsComparer;

namespace Aksio.Integration.for_ImportBuilderExtensions.given
{
    public class no_changes : Specification
    {
        protected IImportBuilderFor<Model, ExternalModel> import_builder;
        protected Subject<ImportContext<Model, ExternalModel>> subject;
        protected IObservable<ImportContext<Model, ExternalModel>> context;
        protected Changeset<Model, Model> changeset;
        protected EventsToAppend events_to_append;
        protected Model original_model;
        protected Model modified_model;

        void Establish()
        {
            subject = new Subject<ImportContext<Model, ExternalModel>>();
            import_builder = new ImportBuilderFor<Model, ExternalModel>(subject);
            modified_model = new Model(42, "Forty Two");
            original_model = new Model(42, "Forty Two");
            changeset = new(modified_model, original_model);
            events_to_append = new();
        }
    }
}