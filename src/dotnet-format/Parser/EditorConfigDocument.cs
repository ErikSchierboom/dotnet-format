using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format.Parser
{
    public sealed class EditorConfigDocument
    {
        public EditorConfigDocument(IReadOnlyList<EditorConfigProperty> properties, IReadOnlyList<EditorConfigSection> sections)
            => (Properties, Sections) = (properties, sections);

        public IReadOnlyList<EditorConfigProperty> Properties { get; }
        public IReadOnlyList<EditorConfigSection> Sections { get; }
    }

    public sealed class EditorConfigSection
    {
        public EditorConfigSection(string key, IReadOnlyList<EditorConfigProperty> properties)
            => (Name, Properties) = (key, properties);

        public string Name { get; }
        public IReadOnlyList<EditorConfigProperty> Properties { get; }
    }

    public enum EditorConfigPropertySeverity
    {
        None = 0,
        Silent,
        Suggestion,
        Warning,
        Error
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

    public static class EditorConfigDocumentParser
    {
        public static EditorConfigDocument Parse(string text)
        {
            var editorConfigDocumentSyntaxNode = EditorConfigSyntaxParser.Parse(text);

            string currentSection = null;
            var sections = new List<EditorConfigSection>();
            var rootProperties = new List<EditorConfigProperty>();
            var currentProperties = new List<EditorConfigProperty>();

            var nodes = editorConfigDocumentSyntaxNode.Nodes;
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] is EditorConfigSectionSyntaxNode sectionSyntaxNode)
                {
                    if (currentSection == null)
                        rootProperties.AddRange(currentProperties.ToList());
                    else
                        sections.Add(new EditorConfigSection(currentSection, currentProperties.ToList()));

                    currentSection = sectionSyntaxNode.Name;
                    currentProperties.Clear();
                }
                else if (nodes[i] is EditorConfigPropertySyntaxNode propertySyntaxNode)
                {
                    currentProperties.Add(CreateEditorConfigProperty(propertySyntaxNode));
                }
                else
                {
                    continue;
                }                    
            }

            if (currentSection == null)
                rootProperties.AddRange(currentProperties);
            else
                sections.Add(new EditorConfigSection(currentSection, currentProperties));

            return new EditorConfigDocument(rootProperties, sections);        }

        private static EditorConfigProperty[] CreateEditorConfigProperties(IEnumerable<EditorConfigSyntaxNode> nodes)
            => nodes.OfType<EditorConfigPropertySyntaxNode>().Select(CreateEditorConfigProperty).ToArray();

        private static EditorConfigProperty CreateEditorConfigProperty(EditorConfigPropertySyntaxNode node)
        {
            var lastDoubleColonIndex = node.Value.LastIndexOf(':');
            if (lastDoubleColonIndex == -1)
                return new EditorConfigProperty(node.Name, node.Value);

            var value = node.Value.Substring(0, lastDoubleColonIndex);
            var severity = Enum.Parse<EditorConfigPropertySeverity>(node.Value.Substring(lastDoubleColonIndex + 1), true);
            return new EditorConfigProperty(node.Name, value, severity);
        }

        private static ReadOnlySpan<EditorConfigSyntaxNode> GetSyntaxNodesForCurrentSection(ReadOnlySpan<EditorConfigSyntaxNode> nodes)
        {
            for (var i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] is EditorConfigSectionSyntaxNode)
                    return i == 0 ? ReadOnlySpan<EditorConfigSyntaxNode>.Empty : nodes.Slice(0, i);
            }

            return nodes;
        }
    }
}
