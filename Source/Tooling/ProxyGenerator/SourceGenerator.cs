using Aksio.ProxyGenerator.Syntax;
using Aksio.ProxyGenerator.Templates;
using HandlebarsDotNet;
using Microsoft.CodeAnalysis;

namespace Aksio.ProxyGenerator
{
    /// <summary>
    /// /// Represents a <see cref="ISourceGenerator"/> for generating proxies for frontend use.
    /// </summary>
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
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
                context.ReportDiagnostic(Diagnostics.MissingOutputPath);
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

                var baseApiRoute = routeAttribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;

                var publicInstanceMethods = type.GetPublicInstanceMethodsFrom();

                OutputCommands(type, publicInstanceMethods, baseApiRoute, rootNamespace, outputFolder);
                OutputQueries(context, type, publicInstanceMethods, baseApiRoute, rootNamespace, outputFolder);
            }
        }

        static void OutputCommands(ITypeSymbol type, IEnumerable<IMethodSymbol> methods, string baseApiRoute, string rootNamespace, string outputFolder)
        {
            var targetFolder = GetTargetFolder(type, rootNamespace, outputFolder);
            foreach (var commandMethod in methods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpPostAttribute())))
            {
                var route = GetRoute(baseApiRoute, commandMethod);
                var commandParameter = commandMethod.Parameters[0];
                var commandType = commandParameter.Type;
                var importStatements = new HashSet<ImportStatement>();
                var properties = commandType.GetPropertyDescriptorsFrom(out var additionalImportStatements);
                additionalImportStatements.ForEach(_ => importStatements.Add(_));
                var commandDescriptor = new CommandDescriptor(route, commandType.Name, properties, importStatements);
                var renderedTemplate = TemplateTypes.Command(commandDescriptor);
                if (renderedTemplate != default)
                {
                    Directory.CreateDirectory(targetFolder);
                    var file = Path.Join(targetFolder, $"{commandType.Name}.ts");
                    File.WriteAllText(file, renderedTemplate);
                }
            }
        }

        static void OutputQueries(GeneratorExecutionContext context, ITypeSymbol type, IEnumerable<IMethodSymbol> methods, string baseApiRoute, string rootNamespace, string outputFolder)
        {
            var targetFolder = GetTargetFolder(type, rootNamespace, outputFolder);
            foreach (var queryMethod in methods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpGetAttribute())))
            {
                var modelType = queryMethod.ReturnType;
                var route = GetRoute(baseApiRoute, queryMethod);
                var importStatements = new HashSet<ImportStatement>();
                if (!modelType.IsEnumerable())
                {
                    context.ReportDiagnostic(Diagnostics.QueryIsNotEnumerable($"{type.ToDisplayString()}:{queryMethod.Name}"));
                }
                var actualType = ((INamedTypeSymbol)queryMethod.ReturnType).TypeArguments[0];
                var targetFile = Path.Join(targetFolder, $"{queryMethod.Name}.ts");
                OutputType(actualType, rootNamespace, outputFolder, targetFile, importStatements);

                var queryDescriptor = new QueryDescriptor(route, queryMethod.Name, actualType.Name, importStatements, Array.Empty<QueryArgumentDescriptor>());
                var renderedTemplate = TemplateTypes.Query(queryDescriptor);
                if (renderedTemplate != default)
                {
                    Directory.CreateDirectory(targetFolder);
                    File.WriteAllText(targetFile, renderedTemplate);
                }
            }
        }

        static void OutputType(ITypeSymbol type, string rootNamespace, string outputFolder, string parentFile, HashSet<ImportStatement> parentImportStatements)
        {
            var targetFolder = GetTargetFolder(type, rootNamespace, outputFolder);
            var targetFile = Path.Join(targetFolder, $"{type.Name}.ts");
            var relativeImport = new Uri(parentFile).MakeRelativeUri(new Uri(targetFile));
            var importPath = Path.GetFileNameWithoutExtension(relativeImport.ToString());
            if (Path.GetDirectoryName(targetFile) == Path.GetDirectoryName(parentFile)) importPath = $"./{importPath}";
            parentImportStatements.Add(new ImportStatement(type.Name, importPath));

            var properties = type.GetMembers().Where(_ => !_.IsStatic
                && _ is IPropertySymbol propertySymbol
                && propertySymbol.DeclaredAccessibility == Accessibility.Public).Cast<IPropertySymbol>();

            var typeImportStatements = new HashSet<ImportStatement>();
            var propertyDescriptors = new List<PropertyDescriptor>();

            foreach (var property in properties)
            {
                var targetType = property.Type.GetTypeScriptType(out var additionalImportStatements);
                additionalImportStatements.ForEach(_ => typeImportStatements.Add(_));
                var isEnumerable = property.Type.IsEnumerable();
                if (targetType == TypeSymbolExtensions.AnyType)
                {
                    var actualType = property.Type;
                    if (isEnumerable)
                    {
                        actualType = ((INamedTypeSymbol)property.Type).TypeArguments[0];
                    }
                    OutputType(actualType, rootNamespace, outputFolder, targetFile, typeImportStatements);

                    propertyDescriptors.Add(new PropertyDescriptor(property.Name, actualType.Name, isEnumerable));
                }
                else
                {
                    propertyDescriptors.Add(new PropertyDescriptor(property.Name, targetType, isEnumerable));
                }
            }

            var typeDescriptor = new TypeDescriptor(type.Name, propertyDescriptors, typeImportStatements);
            var renderedTemplate = TemplateTypes.Type(typeDescriptor);
            if (renderedTemplate != default)
            {
                Directory.CreateDirectory(targetFolder);
                File.WriteAllText(targetFile, renderedTemplate);
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