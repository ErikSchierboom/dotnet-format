﻿namespace DotNet.Format.Parser
{
    public sealed class EditorConfigCommentSyntaxNode : EditorConfigSyntaxNode
    {
        public EditorConfigCommentSyntaxNode(char indicator, string comment) => (Indicator, Comment) = (indicator, comment);

        public char Indicator { get; }
        public string Comment { get; }
    }
}
