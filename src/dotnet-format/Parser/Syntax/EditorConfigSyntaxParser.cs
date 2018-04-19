using Sprache;

namespace DotNet.Format.Parser.Syntax
{
    public static class EditorConfigSyntaxParser
    {
        public static EditorConfigDocumentSyntaxNode Parse(string text) => EditorConfigDocumentGrammar.Document.Parse(text);
    }
}
