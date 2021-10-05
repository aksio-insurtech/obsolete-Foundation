# Events

Events are recognized by their type, not the .NET CLR Type, but its unique identifier.
The CLR type is just the vessel it shows up in when one is committing or reacting
to an event. In its nature, events in an event sourced system are immutable.
With C# 9 we got the construct `record` which is perfect for this.

Below is an example of what an event could be:

```csharp
using Dolittle.SDK.Events;

[EventType("aa4e1a9e-c481-4a2a-abe8-4799a6bbe3b7")]
public record EmployeeRegistered(string FirstName, string LastName);
```

## EventLog

To save an event to the event log, all you need is to take a dependency to `IEventLog`
and call the appropriate `Append` method.

```csharp
using Aksio.Events.EventLogs;

[Route("/api/employees")]
public class EmployeesController : Controller
{
    readonly IEventLog _eventLog;

    public EmployeesController(IEventLog eventLog) => _eventLog = eventLog;

    [HttpPost("registration")]
    public async Task Register()
    {
        await _eventLog.Append(Guid.NewGuid(), new EmployeeRegistered(..., ....));
    }
}
```
