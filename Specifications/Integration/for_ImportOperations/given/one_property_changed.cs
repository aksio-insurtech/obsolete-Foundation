namespace Aksio.Integration.for_ImportOperations.given
{
    public class one_property_changed : all_dependencies
    {
        protected Model initial;
        protected Model mapped;
        protected ExternalModel incoming;
        protected ImportOperations<Model, ExternalModel> operations;

        void Establish()
        {
            initial = new(42, "Forty Two");
            incoming = new(43, "Forty Two");
            mapped = new(incoming.SomeInteger, incoming.SomeString);

            projection.Setup(_ => _.GetById(key)).Returns(initial);
            mapper.Setup(_ => _.Map<Model>(incoming)).Returns(mapped);

            operations = new(
                adapter.Object,
                projection.Object,
                mapper.Object,
                event_log.Object
            );
        }
    }
}