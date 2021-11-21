using Concepts;
using Concepts.Accounts;
using Events.Accounts.Debit;

namespace Read.Accounts.Debit
{
    [Projection("d661904f-15e0-4a96-a0cc-c7389635e4cd")]
    public class DebitAccountLatestTransactionsProjection : IProjectionFor<DebitAccountLatestTransactions>
    {
        public void Define(IProjectionBuilderFor<DebitAccountLatestTransactions> builder) =>
            builder
                .Children(_ => _.Transactions, _ => _
                    .From<DepositToDebitAccountPerformed>(b => b
                        .Set(model => model.Amount).To(@event => @event.Amount))
                    .From<WithdrawalFromDebitAccountPerformed>(b => b
                        .Set(model => model.Amount).To(@event => @event.Amount)));
    }
}