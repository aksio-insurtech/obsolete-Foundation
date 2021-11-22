using Microsoft.AspNetCore.Mvc.Filters;

namespace Aksio.Queries
{
    /// <summary>
    /// Defines a system that translates <see cref="IObservable{T}"/> to web sockets.
    /// </summary>
    public interface IObservableSocket
    {
        /// <summary>
        /// Handle the action context and result from the action.
        /// </summary>
        /// <param name="context"><see cref="ActionExecutingContext"/> to handle for.</param>
        /// <param name="result">The result from the action.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Handle(ActionExecutingContext context, object? result);
    }
}
