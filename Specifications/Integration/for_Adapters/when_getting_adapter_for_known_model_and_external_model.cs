namespace Aksio.Integration.for_Adapters
{
    public class when_getting_adapter_for_known_model_and_external_model : given.one_adapter
    {
        IAdapterFor<Model, ExternalModel> result;

        void Because() => result = adapters.GetFor<Model, ExternalModel>();

        [Fact] void should_return_expected_adapter() => result.ShouldEqual(adapter.Object);
    }
}