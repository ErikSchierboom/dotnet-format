using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.Format.Formatting
{
    public static class GlobbingFormatter
    {
        public static async Task Format(DirectoryInfo root)
        {
            var editorConfigDocuments = new EditorConfigDocumentCollection(root);

            var csharpFiles = new CSharpFileCollection(root);

            foreach (var csharpFile in csharpFiles)
            {
                var editorConfigDocument = editorConfigDocuments.GetForFile(csharpFile);
                var formattingOptions = FormattingOptions.Create(csharpFile, editorConfigDocument);
                await Formatter.Format(csharpFile, formattingOptions);
            }

            // TODO: do bulk change Task.WhenAll

            Console.WriteLine($"Formatted {csharpFiles.Count()} file(s)");
        }
    }
}
