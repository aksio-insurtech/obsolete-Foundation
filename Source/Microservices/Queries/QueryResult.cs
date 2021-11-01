using System.Collections;

namespace Aksio.Queries
{
    /// <summary>
    /// Represents the result coming from performing a query.
    /// </summary>
    /// <param name="Items">Items returned by the query.</param>
    /// <param name="IsSuccess">Whether or not everything is Ok.</param>
    public record QueryResult(IEnumerable Items, bool IsSuccess);
}
