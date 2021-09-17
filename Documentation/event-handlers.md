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

## Middlewares

If you want to get a callback for every handle method that gets called, you can implement what is known as a **Event Handler Middleware**.
They are discovered automatically from their interface type and configured at startup.

For instance, lets say you want to create a middleware that logs something before and after handle:

```csharp
using Aksio.Events.Handling;
using Dolittle.SDK.Events;

public class MyEventHandlerMiddleware : IEventHandlerMiddleware
{
    readonly ILogger<MyEventHandlerMiddleware> _logger;

    public MyEventHandlerMiddleware(ILogger<MyEventHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(EventContext eventContext, object @event, NextEventHandlerMiddleware next)
    {
        var before = DateTime.UtcNow;
        await next().ConfigureAwait(false);
        var after = DateTime.UtcNow;
        var delta = after.Subtract(before);
        _logger.LogInformation("It took {time} milliseconds to run the event handler for type {event}", delta.TotalMilliseconds, @event.GetType().Name);
    }
}
```
