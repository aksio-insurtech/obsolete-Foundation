using Concepts;
using Concepts.Accounts;
using Events.Accounts.Debit;

namespace Read.Accounts.Debit
{
    [Projection("d1bb5522-5512-42ce-938a-d176536bb01d")]
    public class DebitAccountProjection : IProjectionFor<DebitAccount>
    {
        public void Define(IProjectionBuilderFor<DebitAccount> builder) =>
            builder.From<DebitAccountOpened>(_ => _
                .Set(model => model.Name).To(@event => @event.Name)
                .Set(model => model.Owner).To(@event => @event.Owner));
    }
}