using MongoDB.Bson;

namespace Read.Accounts.Debit
{
    [Route("/api/accounts/debit")]
    public class Accounts : Controller
    {
        readonly IMongoCollection<DebitAccount> _collection;

        public Accounts(IMongoCollection<DebitAccount> collection) => _collection = collection;

        [HttpGet]
        public IEnumerable<DebitAccount> AllAccounts() => _collection.Find(_ => true).ToList();

        [HttpGet("starting-with")]
        public IEnumerable<DebitAccount> StartingWith([FromQuery] string? filter)
        {
            var filterDocument = Builders<DebitAccount>
                .Filter
                .Regex("name", $"^{filter ?? string.Empty}.*");

            return _collection.Find(filterDocument).ToList();
        }
    }
}