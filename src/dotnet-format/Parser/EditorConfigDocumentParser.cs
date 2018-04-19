using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format.Parser
{
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
                        sections.Add(new EditorConfigSection(currentSection, new EditorConfigPropertyCollection(currentProperties.ToList())));

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
                sections.Add(new EditorConfigSection(currentSection, new EditorConfigPropertyCollection(currentProperties.ToList())));

            return new EditorConfigDocument(new EditorConfigPropertyCollection(rootProperties), new EditorConfigSectionCollection(sections));
        }

        private static EditorConfigProperty[] CreateEditorConfigProperties(IEnumerable<EditorConfigSyntaxNode> nodes)
            => nodes.OfType<EditorConfigPropertySyntaxNode>().Select(CreateEditorConfigProperty).ToArray();

        private static EditorConfigProperty CreateEditorConfigProperty(EditorConfigPropertySyntaxNode node)
        {
            var lastDoubleColonIndex = node.Value.LastIndexOf(':');
            if (lastDoubleColonIndex == -1)
                return new EditorConfigProperty(node.Name, node.Value);

            return new EditorConfigProperty(node.Name, node.Value.Substring(0, lastDoubleColonIndex));
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
