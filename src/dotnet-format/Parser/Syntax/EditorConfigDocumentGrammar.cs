using Sprache;
using System.Linq;

namespace DotNet.Format.Parser.Syntax
{
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
}
