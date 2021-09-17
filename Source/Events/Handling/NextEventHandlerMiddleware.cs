namespace Aksio.Events.Handling
{
    /// <summary>
    /// The delegate that is called from a middleware.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public delegate Task NextEventHandlerMiddleware();
}