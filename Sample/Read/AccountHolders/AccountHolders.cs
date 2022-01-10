using MongoDB.Bson;

namespace Read.AccountHolders
{
    #pragma warning disable MA0049
    [Route("/api/accountholders")]
    public class AccountHolders : Controller
    {
        readonly IMongoCollection<AccountHolder> _collection;

        public AccountHolders(IMongoCollection<AccountHolder> collection) => _collection = collection;

        [HttpGet]
        public IEnumerable<AccountHolder> AllAccountHolders() => _collection.Find(_ => true).ToList();

        [HttpGet("starting-with")]
        public async Task<IEnumerable<AccountHolder>> AccountHoldersStartingWith([FromQuery] string? filter)
        {
            var filterDocument = Builders<AccountHolder>
                .Filter
                .Regex("firstName", $"^{filter ?? string.Empty}.*");

            var result = await _collection.FindAsync(filterDocument);
            return result.ToList();
        }
    }
}