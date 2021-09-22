namespace Sample
{
    internal static partial class MyEventHandlersLogMessages
    {
        [LoggerMessage(0, LogLevel.Information, "We're here : {Content}")]
        internal static partial void MyEventHandled(this ILogger logger, int content);
    }
}