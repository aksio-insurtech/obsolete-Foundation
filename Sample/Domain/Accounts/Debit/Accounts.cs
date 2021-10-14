using Concepts;
using Concepts.Accounts;
using Events.Accounts.Debit;

namespace Domain.Accounts.Debit
{
    [Route("/api/accounts/debit")]
    public class Accounts : Controller
    {
        readonly IEventLog _eventLog;

        public Accounts(IEventLog eventLog) => _eventLog = eventLog;

        [HttpPost]
        public Task Create([FromBody] CreateDebitAccount create) => _eventLog.Append(create.AccountId, new DebitAccountOpened(create.Name, create.Owner));
    }
}