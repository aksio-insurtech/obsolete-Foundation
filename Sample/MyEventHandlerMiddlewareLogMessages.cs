namespace Sample
{
    internal static partial class MyEventHandlerMiddlewareLogMessages
    {
        [LoggerMessage(0, LogLevel.Information, "It took {Time} milliseconds to run the event handler for type {EventType}")]
        internal static partial void HandlerTiming(this ILogger logger, double time, string eventType);
    }
}