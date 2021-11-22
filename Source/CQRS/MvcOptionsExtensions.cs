using Aksio.Commands;
using Aksio.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for working with <see cref="MvcOptions"/>.
    /// </summary>
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Add CQRS setup.
        /// </summary>
        /// <param name="options"><see cref="MvcOptions"/> to build on.</param>
        /// <returns><see cref="MvcOptions"/> for building continuation.</returns>
        public static MvcOptions AddCQRS(this MvcOptions options)
        {
            options.Filters.Add(new CommandActionFilter());
            options.Filters.Add(new QueryActionFilter());
            return options;
        }
    }
}
