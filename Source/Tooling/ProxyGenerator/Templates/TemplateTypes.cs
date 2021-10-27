using HandlebarsDotNet;

namespace Aksio.ProxyGenerator.Templates
{
    /// <summary>
    /// Holds all the available templates.
    /// </summary>
    public static class TemplateTypes
    {
        /// <summary>
        /// The template for a type.
        /// </summary>
        public static readonly HandlebarsTemplate<object, object> Type = Handlebars.Compile(GetTemplate("Type"));

        /// <summary>
        /// The template for a command.
        /// </summary>
        public static readonly HandlebarsTemplate<object, object> Command = Handlebars.Compile(GetTemplate("Command"));

        /// <summary>
        /// The template for a query.
        /// </summary>
        public static readonly HandlebarsTemplate<object, object> Query = Handlebars.Compile(GetTemplate("Query"));

        static TemplateTypes()
        {
            Handlebars.RegisterHelper("camelcase", (writer, _, parameters) => writer.WriteSafeString(parameters[0].ToString()!.ToCamelCase()));
        }

        static string GetTemplate(string name)
        {
            var rootType = typeof(TemplateTypes);
            var stream = rootType.Assembly.GetManifestResourceStream($"{rootType.Namespace}.{name}.hbs");
            if (stream != default)
            {
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            return string.Empty;
        }
    }
}