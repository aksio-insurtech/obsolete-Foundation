namespace Read.Accounts.Debit
{
    [Route("/api/accounts/debit")]
    public class Accounts : Controller
    {
        readonly IMongoCollection<DebitAccount> _collection;

        public Accounts(IMongoCollection<DebitAccount> collection) => _collection = collection;

        [HttpGet]
        public IEnumerable<DebitAccount> AllAccounts() => _collection.Find(_ => true).ToList();
    }
}