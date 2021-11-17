using AutoMapper;

namespace Aksio.Integration.for_Adapters
{
    public class when_getting_mapper_for_known_model_and_external_model : given.one_adapter
    {
        Mock<IMapper> mapper;
        IMapper result;

        void Establish()
        {
            mapper = new();
            mapper_factory.Setup(_ => _.CreateFor(adapter.Object)).Returns(mapper.Object);
        }

        void Because() => result = adapters.GetMapperFor<Model, ExternalModel>();

        [Fact] void should_return_expected_mapper() => result.ShouldEqual(mapper.Object);
    }
}