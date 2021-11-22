using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aksio.Queries
{
    /// <summary>
    /// Represents an implementation of <see cref="IObservableSocket"/>.
    /// </summary>
    /// <typeparam name="T">Type of value for the observable.</typeparam>
    public class ObservableSocket<T> : IObservableSocket
    {
        /// <inheritdoc/>
        public async Task Handle(ActionExecutingContext context, object? result)
        {
            if (result is null || result is not IObservable<T>) return;

            var observable = (result as IObservable<T>)!;

            using var webSocket = await context.HttpContext.WebSockets.AcceptWebSocketAsync();
            var subscription = observable.Subscribe(_ =>
            {
                var json = JsonSerializer.Serialize(_);
                var message = Encoding.UTF8.GetBytes(json);

                webSocket.SendAsync(new ArraySegment<byte>(message, 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            });

            var buffer = new byte[1024 * 4];
            var received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!received.CloseStatus.HasValue)
            {
                received = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(received.CloseStatus.Value, received.CloseStatusDescription, CancellationToken.None);
            subscription.Dispose();
        }
    }
}
