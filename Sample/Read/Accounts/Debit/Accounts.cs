using System.Reactive.Subjects;
using MongoDB.Bson;
using Timer = System.Timers.Timer;

namespace Read.Accounts.Debit
{
    [Route("/api/accounts/debit")]
    public class Accounts : Controller
    {
        readonly IMongoCollection<DebitAccount> _accountsCollection;
        readonly IMongoCollection<DebitAccountLatestTransactions> _latestTransactionsCollection;

        public Accounts(
            IMongoCollection<DebitAccount> accountsCollection,
            IMongoCollection<DebitAccountLatestTransactions> latestTransactionsCollections)
        {
            _accountsCollection = accountsCollection;
            _latestTransactionsCollection = latestTransactionsCollections;
        }

        [HttpGet]
        public ClientObservable<IEnumerable<DebitAccount>> AllAccounts()
        {
            var observable = new ClientObservable<IEnumerable<DebitAccount>>();
            var accounts = _accountsCollection.Find(_ => true).ToList();

#pragma warning disable CA2000
            var timer = new Timer(1000);
            timer.Elapsed += (sender, e) => observable.OnNext(accounts);
            timer.Start();

            observable.ClientDisconnected = () => timer.Dispose();

            return observable;
        }

        [HttpGet("starting-with")]
        public async Task<IEnumerable<DebitAccount>> StartingWith([FromQuery] string? filter)
        {
            var filterDocument = Builders<DebitAccount>
                .Filter
                .Regex("name", $"^{filter ?? string.Empty}.*");

            var result = await _accountsCollection.FindAsync(filterDocument);
            return result.ToList();
        }

        [HttpGet("latest-transactions/{accountId}")]
        public DebitAccountLatestTransactions LatestTransactions([FromRoute] Guid accountId)
        {
            var items = _latestTransactionsCollection.Find(_ => _.Id == accountId).ToList();
            if (items.Count == 0) return null!;
            return items[0];
        }
    }
}