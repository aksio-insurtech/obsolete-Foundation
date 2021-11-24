using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aksio.Commands
{
    /// <summary>
    /// Represents a <see cref="IAsyncActionFilter"/> for providing a proper <see cref="CommandResult"/> for post actions.
    /// </summary>
    public class CommandActionFilter : IAsyncActionFilter
    {
        /// <inheritdoc/>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (context.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                result.Result = new ObjectResult(new CommandResult(true));
            }
        }
    }
}
