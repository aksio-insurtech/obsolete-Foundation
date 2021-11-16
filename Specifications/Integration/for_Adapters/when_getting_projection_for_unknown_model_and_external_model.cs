namespace Aksio.Integration.for_Adapters
{
    public class when_getting_projection_for_unknown_model_and_external_model : given.no_adapters
    {
        Exception result;

        void Because() => result = Catch.Exception(() => adapters.GetProjectionFor<string, object>());

        [Fact] void should_throw_missing_adapter_for_model_and_external_model() => result.ShouldBeOfExactType<MissingAdapterForModelAndExternalModel>();
    }
}