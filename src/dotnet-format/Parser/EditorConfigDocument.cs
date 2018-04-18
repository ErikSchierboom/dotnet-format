using System.Collections.Generic;

namespace DotNet.Format.Parser
{
    public sealed class EditorConfigDocument
    {
        public EditorConfigDocument(IEnumerable<EditorConfigProperty> properties, IEnumerable<EditorConfigSection> sections)
            => (Properties, Sections) = (properties, sections);

        public IEnumerable<EditorConfigProperty> Properties { get; }
        public IEnumerable<EditorConfigSection> Sections { get; }
    }

    public sealed class EditorConfigSection
    {
        public EditorConfigSection(string key, IEnumerable<EditorConfigProperty> properties)
            => (Name, Properties) = (key, properties);

        public string Name { get; }
        public IEnumerable<EditorConfigProperty> Properties { get; }
    }

    public sealed class EditorConfigProperty
    {
        public EditorConfigProperty(string name, string value, EditorConfigPropertySeverity? severity = null)
            => (Name, Value, Severity) = (name, value, severity);

        public string Name { get; }
        public string Value { get; }
        public EditorConfigPropertySeverity? Severity { get; }

        public override string ToString()
        {
            if (Severity == null)
                return $"{Name} = {Value}";

            return $"{Name} = {Value}:{Severity}";
        }
    }

    public enum EditorConfigPropertySeverity
    {
        None = 0,
        Silent,
        Suggestion,
        Warning,
        Error
    }

    public class EditorConfigDocumentParser
    {
        public EditorConfigDocument Parse(string text)
        {
            var editorConfigDocumentSyntaxNode = EditorConfigSyntaxParser.Parse(text);

            return null;

            return new EditorConfigDocument(
                new[] { new EditorConfigProperty("root", "true") },
                new[]
                {
                    new EditorConfigSection("*",
                        new []
                        {
                            new EditorConfigProperty("indent_style", "space"),
                            new EditorConfigProperty("indent_size", "2")
                        }),
                    new EditorConfigSection("*.cs", new []
                        {
                            new EditorConfigProperty("indent_size", "4"),
                            new EditorConfigProperty("csharp_prefer_braces", "true", EditorConfigPropertySeverity.None),
                            new EditorConfigProperty("csharp_style_throw_expression", "false", EditorConfigPropertySeverity.Suggestion)
                        })
                }
            );
        }

        
    }
}
