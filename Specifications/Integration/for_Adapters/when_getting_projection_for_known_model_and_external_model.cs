namespace Aksio.Integration.for_Adapters
{
    public class when_getting_projection_for_known_model_and_external_model : given.one_adapter
    {
        Mock<IAdapterProjectionFor<Model>> adapter_projection;
        IAdapterProjectionFor<Model> result;

        void Establish()
        {
            adapter_projection = new();
            projection_factory.Setup(_ => _.CreateFor(adapter.Object)).Returns(adapter_projection.Object);
        }

        void Because() => result = adapters.GetProjectionFor<Model, ExternalModel>();

        [Fact] void should_return_expected_projection() => result.ShouldEqual(adapter_projection.Object);
    }
}