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
