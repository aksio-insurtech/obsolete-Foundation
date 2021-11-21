using Microsoft.AspNetCore.Mvc;
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
