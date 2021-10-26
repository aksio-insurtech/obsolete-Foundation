using Microsoft.CodeAnalysis;

namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// Extension methods for working with well known attribute types.
    /// </summary>
    public static class WellKnownAttributeExtensions
    {
        const string HttpPostAttribute = "Microsoft.AspNetCore.Mvc.HttpPostAttribute";
        const string HttpGetAttribute = "Microsoft.AspNetCore.Mvc.HttpGetAttribute";
        const string RouteAttribute = "Microsoft.AspNetCore.Mvc.RouteAttribute";

        /// <summary>
        /// Get the route attribute.
        /// </summary>
        /// <param name="type">Type to get it from.</param>
        /// <returns>Attribute, default if it wasn't there.</returns>
        public static AttributeData? GetRouteAttribute(this ISymbol type)
        {
            return type.GetAttributes().FirstOrDefault(_ => _.AttributeClass?.ToString() == RouteAttribute);
        }

        /// <summary>
        /// Get the HTTP Post attribute.
        /// </summary>
        /// <param name="type">Type to get it from.</param>
        /// <returns>Attribute, default if it wasn't there.</returns>
        public static AttributeData? GetHttpPostAttribute(this ISymbol type)
        {
            return type.GetAttributes().FirstOrDefault(_ => _.AttributeClass?.ToString() == HttpPostAttribute);
        }

        /// <summary>
        /// Get the HTTP Get attribute.
        /// </summary>
        /// <param name="type">Type to get it from.</param>
        /// <returns>Attribute, default if it wasn't there.</returns>
        public static AttributeData? GetHttpGetAttribute(this ISymbol type)
        {
            return type.GetAttributes().FirstOrDefault(_ => _.AttributeClass?.ToString() == HttpGetAttribute);
        }

        /// <summary>
        /// Check whether or not a symbol is an HttpPost attribute.
        /// </summary>
        /// <param name="symbol">Symbol to check.</param>
        /// <returns>True if it is, false if not.</returns>
        public static bool IsHttpPostAttribute(this AttributeData symbol)
        {
            return symbol.AttributeClass?.ToString() == HttpPostAttribute;
        }

        /// <summary>
        /// Check whether or not a symbol is an HttpGet attribute.
        /// </summary>
        /// <param name="symbol">Symbol to check.</param>
        /// <returns>True if it is, false if not.</returns>
        public static bool IsHttpGetAttribute(this AttributeData symbol)
        {
            return symbol.AttributeClass?.ToString() == HttpGetAttribute;
        }
    }
}