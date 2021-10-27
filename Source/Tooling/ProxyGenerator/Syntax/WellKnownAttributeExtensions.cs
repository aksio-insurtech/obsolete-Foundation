using Microsoft.CodeAnalysis;

namespace Aksio.ProxyGenerator.Syntax
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
        /// Gets the route for the method coming from HttpPost/HttpGet attributes - which typically is in addition to the route information used by the Route attribute.
        /// </summary>
        /// <param name="type">Type to get it from.</param>
        /// <returns>The route, if any - can be empty.</returns>
        public static string GetMethodRoute(this ISymbol type)
        {
            var attribute = type.GetAttributes().FirstOrDefault(_ =>
            {
                var attributeName = _.AttributeClass?.ToString();
                return attributeName == HttpGetAttribute || attributeName == HttpPostAttribute;
            });

            if (attribute != default && attribute!.ConstructorArguments.Length == 1)
            {
                return attribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
            }

            return string.Empty;
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