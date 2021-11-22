using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aksio.Queries
{
    /// <summary>
    /// Represents a <see cref="IAsyncActionFilter"/> for providing a proper <see cref="QueryResult"/> for post actions.
    /// </summary>
    public class QueryActionFilter : IAsyncActionFilter
    {
        /// <inheritdoc/>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.WebSockets.IsWebSocketRequest &&
            context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            context.HttpContext.Request.Method == HttpMethod.Get.Method)
            {
                var result = await next();
                if (result.Result is ObjectResult objectResult &&
                objectResult.Value is not null &&
                controllerActionDescriptor.MethodInfo.ReturnType.GenericTypeArguments.Length == 1)
                {
                    if (context.HttpContext.Items.ContainsKey("WebSocket")) return;
                    context.HttpContext.Items["WebSocket"] = true;
                    var observableSocketType = typeof(ObservableSocket<>).MakeGenericType(objectResult.Value.GetType().GetGenericArguments());
                    var observableSocket = Activator.CreateInstance(observableSocketType) as IObservableSocket;
                    await observableSocket!.Handle(context, objectResult.Value);
                    result.Result = null;
                }
            }
            else
            {
                var result = await next();
                if (context.HttpContext.Request.Method == HttpMethod.Get.Method &&
                    result.Result is ObjectResult objectResult &&
                    objectResult.Value is not QueryResult)
                {
                    result.Result = new ObjectResult(new QueryResult(objectResult.Value!, true));
                }
            }
        }
    }
}
