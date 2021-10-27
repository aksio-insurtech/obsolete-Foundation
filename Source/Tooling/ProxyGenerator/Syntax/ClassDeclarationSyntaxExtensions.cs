using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aksio.ProxyGenerator.Syntax
{
    /// <summary>
    /// Extension methods for working with <see cref="ClassDeclarationSyntax"/>.
    /// </summary>
    public static class ClassDeclarationSyntaxExtensions
    {
        const string _nestedClassDelimiter = "+";
        const string _namespaceClassDelimiter = ".";

        /// <summary>
        /// Get the fully qualified name of a <see cref="ClassDeclarationSyntax"/>.
        /// </summary>
        /// <param name="source"><see cref="ClassDeclarationSyntax"/> to get from.</param>
        /// <returns>Fully qualified name.</returns>
        public static string GetFullName(this ClassDeclarationSyntax source)
        {
            Contract.Requires(source != null);

            var items = new List<string>();
            var parent = source!.Parent;
            while (parent.IsKind(SyntaxKind.ClassDeclaration))
            {
                var parentClass = parent as ClassDeclarationSyntax;
                Contract.Assert(parentClass != null);
                items.Add(parentClass.Identifier.Text);

                parent = parent.Parent;
            }

            var nameSpace = parent as NamespaceDeclarationSyntax;
            Contract.Assert(nameSpace != null);
            var stringBuilder = new StringBuilder().Append(nameSpace.Name).Append(_namespaceClassDelimiter);
            items.Reverse();
            items.ForEach(i => stringBuilder.Append(i).Append(_nestedClassDelimiter));
            stringBuilder.Append(source.Identifier.Text);

            return stringBuilder.ToString();
        }
    }
}