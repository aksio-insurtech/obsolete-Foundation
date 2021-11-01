using Aksio.Events.EventLogs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Sample
{
    [Route("/something")]
    public class MyController : Controller
    {
        readonly IEventLog _eventLog;
        readonly IMongoDatabase _database;

        public MyController(IEventLog eventLog, IMongoDatabase database)
        {
            _eventLog = eventLog;
            _database = database;
        }

        [HttpGet]
        public async Task Something()
        {
            var collection = _database.GetCollection<Employee>();
            await collection.FindAsync(_ => true);
            await _eventLog.Append(Guid.NewGuid(), new MyEvent(42));
        }
    }
}
