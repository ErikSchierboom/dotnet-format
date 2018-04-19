using System.IO;
using System.Linq;

namespace DotNet.Format.Formatting
{
    public sealed class FormattingOptions
    {
        public string NewLine { get; set; } = "\n";
        public bool UseTabs { get; set; } = false;
        public int TabSize { get; set; } = 4;
        public int IndentationSize { get; set; } = 4;
        public bool SpacingAfterMethodDeclarationName { get; set; } = false;
        public bool SpaceWithinMethodDeclarationParenthesis { get; set; } = false;
        public bool SpaceBetweenEmptyMethodDeclarationParentheses { get; set; } = false;
        public bool SpaceAfterMethodCallName { get; set; } = false;
        public bool SpaceWithinMethodCallParentheses { get; set; } = false;
        public bool SpaceBetweenEmptyMethodCallParentheses { get; set; } = false;
        public bool SpaceAfterControlFlowStatementKeyword { get; set; } = true;
        public bool SpaceWithinExpressionParentheses { get; set; } = false;
        public bool SpaceWithinCastParentheses { get; set; } = false;
        public bool SpaceWithinOtherParentheses { get; set; } = false;
        public bool SpaceAfterCast { get; set; } = false;
        public bool SpacesIgnoreAroundVariableDeclaration { get; set; } = false;
        public bool SpaceBeforeOpenSquareBracket { get; set; } = false;
        public bool SpaceBetweenEmptySquareBrackets { get; set; } = false;
        public bool SpaceWithinSquareBrackets { get; set; } = false;
        public bool SpaceAfterColonInBaseTypeDeclaration { get; set; } = true;
        public bool SpaceAfterComma { get; set; } = true;
        public bool SpaceAfterDot { get; set; } = false;
        public bool SpaceAfterSemicolonsInForStatement { get; set; } = true;
        public bool SpaceBeforeColonInBaseTypeDeclaration { get; set; } = true;
        public bool SpaceBeforeComma { get; set; } = false;
        public bool SpaceBeforeDot { get; set; } = false;
        public bool SpaceBeforeSemicolonsInForStatement { get; set; } = false;
        public string SpacingAroundBinaryOperator { get; set; } = "single";
        public bool IndentBraces { get; set; } = false;
        public bool IndentBlock { get; set; } = true;
        public bool IndentSwitchSection { get; set; } = true;
        public bool IndentSwitchCaseSection { get; set; } = true;
        public string LabelPositioning { get; set; } = "oneLess";
        public bool WrappingPreserveSingleLine { get; set; } = true;
        public bool WrappingKeepStatementsOnSingleLine { get; set; } = true;
        public bool NewLinesForBracesInTypes { get; set; } = true;
        public bool NewLinesForBracesInMethods { get; set; } = true;
        public bool NewLinesForBracesInProperties { get; set; } = true;
        public bool NewLinesForBracesInAccessors { get; set; } = true;
        public bool NewLinesForBracesInAnonymousMethods { get; set; } = true;
        public bool NewLinesForBracesInControlBlocks { get; set; } = true;
        public bool NewLinesForBracesInAnonymousTypes { get; set; } = true;
        public bool NewLinesForBracesInObjectCollectionArrayInitializers { get; set; } = true;
        public bool NewLinesForBracesInLambdaExpressionBody { get; set; } = true;
        public bool NewLineForElse { get; set; } = true;
        public bool NewLineForCatch { get; set; } = true;
        public bool NewLineForFinally { get; set; } = true;
        public bool NewLineForMembersInObjectInit { get; set; } = true;
        public bool NewLineForMembersInAnonymousTypes { get; set; } = true;
        public bool NewLineForClausesInQuery { get; set; } = true;

        public static FormattingOptions Create(FileInfo file, EditorConfigDocument editorConfigDocument)
        {
            if (editorConfigDocument == null)
                return new FormattingOptions();

            var editorConfigProperties = editorConfigDocument
                .GetMergedMatchingProperties(file.Name)
                .ToDictionary(property => property.Name, property => property.Value);

            return new FormattingOptions().WithEditorConfigProperties(editorConfigProperties);
        }        
    }
}
