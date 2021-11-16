using Cratis.Events;
using Cratis.Events.Projections;
using Cratis.Events.Projections.Json;
using Microsoft.Extensions.Logging;
using IEventStore = Cratis.Extensions.Dolittle.EventStore.IEventStore;

namespace Aksio.Integration.for_AdapterProjectionFactory
{
    public class when_creating_for_adapter : Specification
    {
        Mock<IEventStore> event_store;
        Mock<IEventTypes> event_types;
        Mock<IJsonProjectionSerializer> projection_serializer;

        Mock<IAdapterFor<Model, ExternalModel>> adapter;

        AdapterProjectionFactory factory;
        IAdapterProjectionFor<Model> result;

        void Establish()
        {
            event_store = new();
            event_types = new();
            projection_serializer = new();
            factory = new(
                event_store.Object,
                event_types.Object,
                projection_serializer.Object,
                Mock.Of<ILoggerFactory>());
            adapter = new();
        }

        void Because() => result = factory.CreateFor(adapter.Object);

        [Fact] void should_define_model() => adapter.Verify(_ => _.DefineModel(IsAny<ProjectionBuilderFor<Model>>()), Once());
        [Fact] void should_return_projection() => result.ShouldNotBeNull();
    }
}