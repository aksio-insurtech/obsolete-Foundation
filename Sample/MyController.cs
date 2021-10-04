using Aksio.Events.EventLogs;
using Microsoft.AspNetCore.Mvc;

namespace Sample
{
    [Route("/something")]
    public class MyController : Controller
    {
        readonly IEventLog _eventLog;

        public MyController(IEventLog eventLog)
        {
            _eventLog = eventLog;
        }

        [HttpGet]
        public async Task Something()
        {
            await _eventLog.Append(Guid.NewGuid(), new MyEvent(42)).ConfigureAwait(false);
        }
    }
}
