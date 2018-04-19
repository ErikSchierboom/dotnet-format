using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Format.Formatting
{
    public static class Formatter
    {
        private static readonly Document document = CreateDocument();
        
        public static async Task Format(FileInfo file, FormattingOptions formattingOptions, CancellationToken cancellationToken = default)
        {
            using (var fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite))
            using (var fileWriter = new StreamWriter(fileStream))
            {
                var documentFormattingOptions = document.Project.Solution.Options.WithChangedFormattingOptions(formattingOptions);
                var unformattedDocument = document.WithText(SourceText.From(fileStream));

                var formattedDocument = await Microsoft.CodeAnalysis.Formatting.Formatter.FormatAsync(unformattedDocument, documentFormattingOptions, cancellationToken);
                var formattedText = await formattedDocument.GetTextAsync(cancellationToken);

                fileStream.SetLength(0);
                formattedText.Write(fileWriter, cancellationToken);
            }
        }

        private static Document CreateDocument() =>
            new AdhocWorkspace()
                .AddProject("AdhocCSharpProject", LanguageNames.CSharp)
                .AddDocument("AdhocCSharpFile", SourceText.From("class A {}"));
    }
}
