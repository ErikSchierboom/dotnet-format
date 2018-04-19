using System;
using System.Collections.Generic;

namespace DotNet.Format.Formatting
{
    public static class FormattingOptionsExtensions
    {
        public static FormattingOptions WithEditorConfigProperties(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            return formattingOptions
                .WithIndentStyle(editorConfigProperties)
                .WithIndentSize(editorConfigProperties)
                .WithTabWith(editorConfigProperties)
                .WithEndOfLine(editorConfigProperties)
                .WithIndentSwitchSection(editorConfigProperties)
                .WithIndentSwitchCaseSection(editorConfigProperties)
                .WithNewLineForElse(editorConfigProperties)
                .WithNewLineForCatch(editorConfigProperties)
                .WithNewLineForFinally(editorConfigProperties)
                .WithNewLineForMembersInObjectInit(editorConfigProperties)
                .WithNewLineForMembersInAnonymousTypes(editorConfigProperties)
                .WithNewLineForClausesInQuery(editorConfigProperties)                
                .WithNewLinesForBracesInAccessors(editorConfigProperties)
                .WithNewLinesForBracesInAnonymousMethods(editorConfigProperties)
                .WithNewLinesForBracesInAnonymousTypes(editorConfigProperties)
                .WithNewLinesForBracesInControlBlocks(editorConfigProperties)
                .WithNewLinesForBracesInLambdaExpressionBody(editorConfigProperties)
                .WithNewLinesForBracesInMethods(editorConfigProperties)
                .WithNewLinesForBracesInObjectCollectionArrayInitializers(editorConfigProperties)
                .WithNewLinesForBracesInProperties(editorConfigProperties)
                .WithNewLinesForBracesInTypes(editorConfigProperties);
        }

        public static FormattingOptions WithIndentStyle(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("indent_style", out var indentStyle))
                formattingOptions.UseTabs = indentStyle.Equals("tab", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithIndentSize(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("indent_size", out var indentSize))
                formattingOptions.IndentationSize = int.Parse(indentSize);

            return formattingOptions;
        }

        public static FormattingOptions WithTabWith(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("tab_width", out var tabWith))
                formattingOptions.TabSize = int.Parse(tabWith);

            return formattingOptions;
        }

        public static FormattingOptions WithEndOfLine(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("end_of_line", out var endOfLine))
                formattingOptions.NewLine = endOfLine.Replace("cr", "\r").Replace("lf", "\n");

            return formattingOptions;
        }

        public static FormattingOptions WithIndentSwitchSection(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_indent_switch_labels", out var enabled))
                formattingOptions.IndentSwitchSection = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithIndentSwitchCaseSection(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_indent_case_contents", out var enabled))
                formattingOptions.IndentSwitchCaseSection = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForElse(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_else", out var enabled))
                formattingOptions.NewLineForElse = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForCatch(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_catch", out var enabled))
                formattingOptions.NewLineForCatch = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForFinally(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_finally", out var enabled))
                formattingOptions.NewLineForFinally = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForMembersInObjectInit(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_members_in_object_initializers", out var enabled))
                formattingOptions.NewLineForMembersInObjectInit = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForMembersInAnonymousTypes(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_members_in_anonymous_types", out var enabled))
                formattingOptions.NewLineForMembersInAnonymousTypes = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLineForClausesInQuery(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_between_query_expression_clauses", out var enabled))
                formattingOptions.NewLineForClausesInQuery = bool.Parse(enabled);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInTypes(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInTypes = enabled.Contains("types", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInMethods(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInMethods = enabled.Contains("methods", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInProperties(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInProperties = enabled.Contains("properties", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInAccessors(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInAccessors = enabled.Contains("accessors", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInAnonymousMethods(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInAnonymousMethods = enabled.Contains("anonymous_methods", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInControlBlocks(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInControlBlocks = enabled.Contains("control_blocks", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInAnonymousTypes(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInAnonymousTypes = enabled.Contains("anonymous_types", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInObjectCollectionArrayInitializers(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers = enabled.Contains("object_collection", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }

        public static FormattingOptions WithNewLinesForBracesInLambdaExpressionBody(this FormattingOptions formattingOptions, Dictionary<string, string> editorConfigProperties)
        {
            if (editorConfigProperties.TryGetValue("csharp_new_line_before_open_brace", out var enabled))
                formattingOptions.NewLinesForBracesInLambdaExpressionBody = enabled.Contains("lambdas", StringComparison.OrdinalIgnoreCase);

            return formattingOptions;
        }
    }
}
