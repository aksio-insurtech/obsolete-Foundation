using Cratis.Events;
using Cratis.Events.Projections.Json;
using Cratis.Types;
using Microsoft.Extensions.Logging;
using IEventStore = Cratis.Extensions.Dolittle.EventStore.IEventStore;

namespace Aksio.Integration.for_Adapters.given
{
    public class all_dependencies : Specification
    {
        protected Mock<ITypes> types;
        protected Mock<IServiceProvider> service_provider;
        protected Mock<IAdapterProjectionFactory> projection_factory;
        protected Mock<IAdapterMapperFactory> mapper_factory;

        void Establish()
        {
            types = new();
            service_provider = new();
            projection_factory = new();
            mapper_factory = new();
        }
    }
}