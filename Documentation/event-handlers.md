# Event Handlers

Event Handlers are classes that holds methods that react to certain events.
The events through its methods it is handling constitutes a [stream](https://dolittle.io/docs/concepts/streams/).

By adding an attribute in front of the handler class, it will automatically be discovered at startup and
the system will figure out which methods will be handling what events.

```csharp
using Dolittle.SDK.Events.Handling;

[EventHandler("a5a55a35-1846-4386-b752-4d9a3da7aa10")]
public class MyEventHandler
{
}
```

The methods you add onto the class will be discovered by convention and the system recognizes the following
signatures:

```csharp
void SynchronousMethodWithoutContext(MyEvent @event);
void SynchronousMethodWithContext(MyEvent @event, EventContext context);
Task AsynchronousMethodWithoutContext(MyEvent @event);
Task AsynchronousMethodWithContext(MyEvent @event, EventContext context);
```

> Note: Both public and non-public methods are supported.
