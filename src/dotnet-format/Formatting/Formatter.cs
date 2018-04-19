using DotNet.Format.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet.Format.Formatting
{
    public class Formatter
    {
        private static readonly Document document = CreateDocument();

        private readonly EditorConfigDocumentCollection editorConfigDocuments;
        private readonly DirectoryInfo root;

        public Formatter(DirectoryInfo root)
        {
            editorConfigDocuments = new EditorConfigDocumentCollection(root);
            this.root = root;
        }
        
        public async Task FormatAllFiles(CancellationToken cancellationToken = default)
        {
            var sourceFiles = new SourceFileCollection(root);

            var stopwatch = Stopwatch.StartNew();
            await Task.WhenAll(sourceFiles.Select(sourceFile => FormatSingleFile(sourceFile, cancellationToken)));
            stopwatch.Stop();

            Console.WriteLine($"Formatted {sourceFiles.Count()} file(s) in {(int)stopwatch.Elapsed.TotalMilliseconds}ms");
        }

        public async Task FormatSingleFile(FileInfo file, CancellationToken cancellationToken = default)
        {
            var editorConfigDocument = editorConfigDocuments.GetForFile(file);
            var formattingOptions = FormattingOptions.Create(file, editorConfigDocument);

            var stopwatch = Stopwatch.StartNew();

            using (var fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite))
            using (var fileWriter = new StreamWriter(fileStream))
            {
                var documentFormattingOptions = document.Project.Solution.Options.WithChangedFormattingOptions(formattingOptions);
                var unformattedDocument = document.WithText(SourceText.From(fileStream));

                var formattedDocument = await Microsoft.CodeAnalysis.Formatting.Formatter.FormatAsync(unformattedDocument, documentFormattingOptions, cancellationToken);
                var formattedText = await formattedDocument.GetTextAsync(cancellationToken);

                fileStream.SetLength(0);
                formattedText.Write(fileWriter, cancellationToken);

                stopwatch.Stop();

                Console.WriteLine($"Formatted .{Path.DirectorySeparatorChar}{file.FullName.Substring(root.FullName.Length + 1)} in {(int)stopwatch.Elapsed.TotalMilliseconds}ms");
            }
        }

        private static Document CreateDocument() =>
            new AdhocWorkspace()
                .AddProject("AdhocCSharpProject", LanguageNames.CSharp)
                .AddDocument("AdhocCSharpFile", SourceText.From("class A {}"));
    }
}
