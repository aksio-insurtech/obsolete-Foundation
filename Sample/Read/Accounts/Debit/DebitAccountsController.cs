namespace Read.Accounts.Debit
{
    [Route("/api/accounts/debit")]
    public class DebitAccountsController : Controller
    {
        readonly IMongoCollection<DebitAccount> _collection;

        public DebitAccountsController(IMongoCollection<DebitAccount> collection) => _collection = collection;

        [HttpGet]
        public IEnumerable<DebitAccount> AllAccounts() => _collection.Find(_ => true).ToList();
    }
}