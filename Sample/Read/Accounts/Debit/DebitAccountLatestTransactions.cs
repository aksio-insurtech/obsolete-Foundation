using Concepts.Accounts;

namespace Read.Accounts.Debit
{
    public record DebitAccountLatestTransactions(AccountId Id, IEnumerable<AccountTransaction> Transactions);
}