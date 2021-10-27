using Aksio.ProxyGenerator.Syntax;
using Aksio.ProxyGenerator.Templates;
using HandlebarsDotNet;
using Microsoft.CodeAnalysis;

namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// Represents a <see cref="ISourceGenerator"/> for generating proxies for frontend use.
    /// </summary>
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        static readonly Diagnostic MissingOutputPath = Diagnostic.Create(
            new DiagnosticDescriptor(
                "AKSIO0001",
                "Missing output path",
                "Missing output path for generating proxies to. Add <AksioProxyOutput/> to your .csproj file. Will not output proxies.",
                "Generation",
                DiagnosticSeverity.Warning,
                true),
            default);

        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = context.SyntaxReceiver as SyntaxReceiver;
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.rootnamespace", out var rootNamespace);
            if (!context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.aksioproxyoutput", out var outputFolder))
            {
                context.ReportDiagnostic(MissingOutputPath);
                return;
            }

            foreach (var classDeclaration in receiver!.Candidates)
            {
                var model = context.Compilation.GetSemanticModel(classDeclaration.SyntaxTree, true);
                if (!(model.GetDeclaredSymbol(classDeclaration) is ITypeSymbol type)) continue;

                if (string.IsNullOrEmpty(rootNamespace))
                {
                    rootNamespace = type.ContainingAssembly.Name!;
                }

                var routeAttribute = type.GetRouteAttribute();
                if (routeAttribute == default) return;

                var targetFolder = GetTargetFolder(type, rootNamespace, outputFolder);
                var baseApiRoute = routeAttribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;

                var publicInstanceMethods = type.GetPublicInstanceMethodsFrom();

                OutputCommands(publicInstanceMethods, baseApiRoute, targetFolder);
                OutputQueries(publicInstanceMethods, baseApiRoute, targetFolder);
            }
        }

        static void OutputCommands(IEnumerable<IMethodSymbol> methods, string baseApiRoute, string targetFolder)
        {
            foreach (var commandMethod in methods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpPostAttribute())))
            {
                var route = GetRoute(baseApiRoute, commandMethod);
                var commandParameter = commandMethod.Parameters[0];
                var commandType = commandParameter.Type;
                var importStatements = new HashSet<ImportStatement>();
                var properties = commandType.GetPropertyDescriptorsFrom(out var additionalImportStatements);
                additionalImportStatements.ForEach(_ => importStatements.Add(_));
                var commandDescriptor = new CommandDescriptor(route, commandType.Name, properties, importStatements);
                var result = TemplateTypes.Command(commandDescriptor);
                if (result != default)
                {
                    Directory.CreateDirectory(targetFolder);
                    var file = Path.Join(targetFolder, $"{commandType.Name}.ts");
                    File.WriteAllText(file, result);
                }
            }
        }

        static void OutputQueries(IEnumerable<IMethodSymbol> methods, string baseApiRoute, string targetFolder)
        {
            foreach (var queryMethod in methods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpGetAttribute())))
            {
                var route = GetRoute(baseApiRoute, queryMethod);

                Console.WriteLine(targetFolder);
            }
        }

        static string GetRoute(string baseApiRoute, IMethodSymbol commandMethod)
        {
            var methodRoute = commandMethod.GetMethodRoute();
            var fullRoute = baseApiRoute;
            if (methodRoute.Length > 0)
            {
                fullRoute = $"{baseApiRoute}/{methodRoute}";
            }

            return fullRoute;
        }

        static string GetTargetFolder(ITypeSymbol type, string rootNamespace, string outputFolder)
        {
            var segments = type.ContainingNamespace.ToDisplayString().Replace(rootNamespace, string.Empty, StringComparison.InvariantCulture)
                                                            .Split(".")
                                                            .Where(_ => _.Length > 0);

            var relativePath = string.Join(Path.DirectorySeparatorChar, segments);
            return Path.Join(outputFolder, relativePath);
        }
    }
}