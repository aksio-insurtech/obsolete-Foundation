using Aksio.Events.EventLogs;
using AutoMapper;

namespace Aksio.Integration.for_Importer
{
    public class when_getting_for_model_and_external_model : Specification
    {
        Mock<IAdapters> adapters;
        Mock<IEventLog> event_log;
        Mock<IAdapterFor<Model, ExternalModel>> adapter;
        Mock<IAdapterProjectionFor<Model>> projection;
        Mock<IMapper> mapper;

        Importer importer;

        IImportOperations<Model, ExternalModel> operations;

        void Establish()
        {
            adapters = new();
            event_log = new();
            adapter = new();
            projection = new();
            mapper = new();

            adapters.Setup(_ => _.GetFor<Model, ExternalModel>()).Returns(adapter.Object);
            adapters.Setup(_ => _.GetProjectionFor<Model, ExternalModel>()).Returns(projection.Object);
            adapters.Setup(_ => _.GetMapperFor<Model, ExternalModel>()).Returns(mapper.Object);

            importer = new(adapters.Object, event_log.Object);
        }

        void Because() => operations = importer.For<Model, ExternalModel>();

        [Fact] void should_get_operations() => operations.ShouldNotBeNull();
        [Fact] void should_hold_correct_adapter() => operations.Adapter.ShouldEqual(adapter.Object);
        [Fact] void should_hold_correct_projection() => operations.Projection.ShouldEqual(projection.Object);
        [Fact] void should_hold_correct_mapper() => operations.Mapper.ShouldEqual(mapper.Object);
    }
}
