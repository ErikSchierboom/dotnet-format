using System.Collections.Generic;

namespace DotNet.Format.Parser.Syntax
{
    public sealed class EditorConfigDocumentSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigDocumentSyntaxNode(IReadOnlyList<EditorConfigSyntaxNode> nodes) => (Nodes) = (nodes);

        public IReadOnlyList<EditorConfigSyntaxNode> Nodes { get; }

        public override string ToString() => string.Join('\n', Nodes);
    }
}
