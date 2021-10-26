using Aksio.ProxyGenerator.Templates;
using HandlebarsDotNet;
using Microsoft.CodeAnalysis;

#pragma warning disable RCS1213, CA1823, IDE0052, SA1201

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

        static readonly Dictionary<string, TargetType> _primitiveTypeMap = new()
        {
            { typeof(string).FullName!, new("string") },
            { typeof(short).FullName!, new("number") },
            { typeof(int).FullName!, new("number") },
            { typeof(long).FullName!, new("number") },
            { typeof(ushort).FullName!, new("number") },
            { typeof(uint).FullName!, new("number") },
            { typeof(ulong).FullName!, new("number") },
            { typeof(float).FullName!, new("number") },
            { typeof(double).FullName!, new("number") },
            { typeof(DateTime).FullName!, new("string") },
            { typeof(DateTimeOffset).FullName!, new("string") },
            { typeof(Guid).FullName!, new("Guid", "@cratis/fundamentals") },
        };

        static readonly HandlebarsTemplate<object, object> _typeTemplate = Handlebars.Compile(GetTemplate("Type"));
        static readonly HandlebarsTemplate<object, object> _commandTemplate = Handlebars.Compile(GetTemplate("Command"));
        static readonly HandlebarsTemplate<object, object> _queryTemplate = Handlebars.Compile(GetTemplate("Query"));

        static SourceGenerator()
        {
            Handlebars.RegisterHelper("camelcase", (writer, _, parameters) => writer.WriteSafeString(parameters[0].ToString()!.ToCamelCase()));
        }

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

                var publicInstanceMethods = type.GetMembers().Where(_ =>
                    !_.IsStatic &&
                    _ is IMethodSymbol methodSymbol &&
                    methodSymbol.DeclaredAccessibility == Accessibility.Public &&
                    methodSymbol.MethodKind != MethodKind.Constructor).Cast<IMethodSymbol>();

                var commands = publicInstanceMethods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpPostAttribute()));
                var queries = publicInstanceMethods.Where(_ => _.GetAttributes().Any(_ => _.IsHttpGetAttribute()));

                var baseApiRoute = routeAttribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty;

                foreach (var command in commands)
                {
                    var postAttribute = command.GetHttpPostAttribute();
                    var commandRoute = baseApiRoute;
                    if (postAttribute!.ConstructorArguments.Length == 1)
                    {
                        commandRoute = $"{baseApiRoute}/{postAttribute.ConstructorArguments[0].Value}";
                    }
                    var commandParameter = command.Parameters[0];
                    var commandType = commandParameter.Type;
                    var importStatements = new HashSet<ImportStatement>();
                    var properties = GetPropertyDescriptorsFrom(commandType, importStatements);
                    var commandDescriptor = new CommandDescriptor(commandRoute, commandType.Name, properties, importStatements);
                    var result = _commandTemplate(commandDescriptor);
                    if (result != default)
                    {
                        Directory.CreateDirectory(targetFolder);
                        var file = Path.Join(targetFolder, $"{commandType.Name}.ts");
                        File.WriteAllText(file, result);
                    }
                }
            }
        }

        static string GetTargetFolder(ITypeSymbol type, string rootNamespace, string outputFolder)
        {
            var segments = type.ContainingNamespace.ToDisplayString().Replace(rootNamespace, string.Empty, StringComparison.InvariantCulture)
                                                            .Split(".")
                                                            .Where(_ => _.Length > 0);

            var relativePath = string.Join(Path.DirectorySeparatorChar, segments);
            return Path.Join(outputFolder, relativePath);
        }

        static string GetTemplate(string name)
        {
            var rootType = typeof(Root);
            var stream = rootType.Assembly.GetManifestResourceStream($"{rootType.Namespace}.{name}.hbs");
            if (stream != default)
            {
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            return string.Empty;
        }

        IEnumerable<PropertyDescriptor> GetPropertyDescriptorsFrom(ITypeSymbol type, HashSet<ImportStatement> importStatements)
        {
            List<PropertyDescriptor> descriptors = new();

            var properties = type.GetMembers().Where(_ => !_.IsStatic
                && _ is IPropertySymbol propertySymbol
                && propertySymbol.DeclaredAccessibility == Accessibility.Public).Cast<IPropertySymbol>();

            return properties.Select(_ =>
                new PropertyDescriptor(
                    _.Name,
                    GetTypeScriptTypeFor(_.GetMethod!.ReturnType, importStatements),
                    false)).ToArray();
        }

        string GetTypeScriptTypeFor(ITypeSymbol symbol, HashSet<ImportStatement> importStatements)
        {
            var baseType = symbol.BaseType;
            if (baseType?.IsGenericType == true &&
                baseType?.ContainingNamespace.ToDisplayString() == "Cratis.Concepts" &&
                baseType?.Name == "ConceptAs")
            {
                symbol = baseType!.TypeArguments[0];
            }

            var typeName = GetTypeName(symbol);
            if (_primitiveTypeMap.ContainsKey(typeName))
            {
                var targetType = _primitiveTypeMap[typeName];

                if (!string.IsNullOrEmpty(targetType.ImportFromModule))
                {
                    importStatements.Add(new(targetType.Type, targetType.ImportFromModule));
                }
                return targetType.Type;
            }
            return "any";
        }

        string GetTypeName(ITypeSymbol symbol)
        {
            return $"{symbol.ContainingNamespace.ToDisplayString()}.{symbol.Name}";
        }
    }
}