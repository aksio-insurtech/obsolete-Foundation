using Microsoft.Extensions.Logging;

namespace Aksio.Integration.for_Adapters.given
{
    public class one_adapter : all_dependencies
    {
        protected Adapters adapters;

        protected Mock<IAdapterFor<Model, ExternalModel>> adapter;

        void Establish()
        {
            adapter = new Mock<IAdapterFor<Model, ExternalModel>>();
            var adapterType = adapter.Object.GetType();
            types.Setup(_ => _.FindMultiple(typeof(IAdapterFor<,>))).Returns(new[] { adapterType });
            service_provider.Setup(_ => _.GetService(adapterType)).Returns(adapter.Object);
            adapters = new(
                types.Object,
                service_provider.Object,
                projection_factory.Object,
                mapper_factory.Object);
        }
    }
}
