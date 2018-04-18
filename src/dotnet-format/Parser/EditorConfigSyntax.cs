using Sprache;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Format.Parser
{
    public abstract class EditorConfigSyntaxNode
    {
    }

    public sealed class EditorConfigCommentSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigCommentSyntaxNode(char indicator, string comment) => (Indicator, Comment) = (indicator, comment);

        public char Indicator { get; }
        public string Comment { get; }

        public override string ToString() => $"{Indicator}{Comment}";
    }

    public sealed class EditorConfigSectionSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigSectionSyntaxNode(string name) => Name = name;

        public string Name { get; }

        public override string ToString() => $"[{Name}]";
    }

    public sealed class EditorConfigPropertySyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigPropertySyntaxNode(string name, string value) => (Name, Value) = (name, value);

        public string Name { get; }
        public string Value { get; }

        public override string ToString() => $"{Name} = {Value}";
    }

    public sealed class EditorConfigDocumentSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigDocumentSyntaxNode(IReadOnlyList<EditorConfigSyntaxNode> nodes) => (Nodes) = (nodes);

        public IReadOnlyList<EditorConfigSyntaxNode> Nodes { get; }

        public override string ToString() => string.Join('\n', Nodes);
    }

    public static class EditorConfigDocumentGrammar
    {
        public static readonly Parser<EditorConfigCommentSyntaxNode> Comment =
            (from indicator in Parse.Chars('#', ';')
             from comment in Parse.AnyChar.Until(Parse.LineTerminator).Text()
             select new EditorConfigCommentSyntaxNode(indicator, comment)).Token();

        public static readonly Parser<string> PropertyName =
            Parse.LetterOrDigit.Or(Parse.Char('_')).AtLeastOnce().Text();

        public static readonly Parser<string> PropertyValue =
            Parse.AnyChar.Until(Parse.LineTerminator).Text();

        public static readonly Parser<EditorConfigPropertySyntaxNode> Property =
            (from name in PropertyName
             from separator in Parse.Char('=').Token()
             from value in PropertyValue
             select new EditorConfigPropertySyntaxNode(name, value)).Token();

        public static readonly Parser<EditorConfigSectionSyntaxNode> Section =
            (from ldelim in Parse.Char('[')
             from name in Parse.CharExcept(']').AtLeastOnce().Text()
             from rdelim in Parse.Char(']')
             select new EditorConfigSectionSyntaxNode(name)).Token();

        public static readonly Parser<EditorConfigDocumentSyntaxNode> Document =
            from nodes in Comment.Or<EditorConfigSyntaxNode>(Section).Or(Property).Many()
            select new EditorConfigDocumentSyntaxNode(nodes.ToArray());
    }

    public static class EditorConfigSyntaxParser
    {
        public static EditorConfigDocumentSyntaxNode Parse(string text) => EditorConfigDocumentGrammar.Document.Parse(text);
    }
}
