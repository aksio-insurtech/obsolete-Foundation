using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// Extension methods for working with <see cref="TypeSyntax"/>.
    /// </summary>
    public static class TypeSyntaxExtensions
    {
        /// <summary>
        /// Get the name from a <see cref="TypeSyntax"/>.
        /// </summary>
        /// <param name="type"><see cref="TypeSyntax"/> to get from.</param>
        /// <returns>The name.</returns>
        public static string GetName(this TypeSyntax type)
        {
            while (type != null)
            {
                switch (type)
                {
                    case IdentifierNameSyntax ins:
                        return ins.Identifier.Text;

                    case QualifiedNameSyntax qns:
                        type = qns.Right;
                        break;

                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}