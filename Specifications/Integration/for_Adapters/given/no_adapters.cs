using Microsoft.Extensions.Logging;

namespace Aksio.Integration.for_Adapters.given
{
    public class no_adapters : all_dependencies
    {
        protected Adapters adapters;

        void Establish() => adapters = new(
                types.Object,
                service_provider.Object,
                projection_factory.Object,
                mapper_factory.Object);
    }
}
