using System.Collections.Generic;

namespace DotNet.Format.Parser
{
    public sealed class EditorConfigDocumentSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigDocumentSyntaxNode(IReadOnlyList<EditorConfigSyntaxNode> nodes) => (Nodes) = (nodes);

        public IReadOnlyList<EditorConfigSyntaxNode> Nodes { get; }
    }
}
