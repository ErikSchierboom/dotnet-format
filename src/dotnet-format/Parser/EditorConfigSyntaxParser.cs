using Sprache;
using System.Threading;

namespace DotNet.Format.Parser
{
    public static class EditorConfigSyntaxParser
    {
        public static EditorConfigDocumentSyntaxNode Parse(string text, CancellationToken cancellationToken = default) 
            => EditorConfigDocumentGrammar.Document.Parse(text);
    }
}
